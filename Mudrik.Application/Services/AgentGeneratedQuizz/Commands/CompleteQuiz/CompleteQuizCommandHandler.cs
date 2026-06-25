using MediatR;
using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.AgentGeneratedQuizz.DTOs;
using Mudrik.Application.Services.Badges.Commands.CheckAndAwardBadges;
using Mudrik.Application.Services.Gamification.Commands.AwardXp;
using Mudrik.Domain.Enums;
using Mudrik.Domain.Models;
using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.AgentGeneratedQuizz.Commands.CompleteQuiz
{
    public class CompleteQuizCommandHandler(IAppDbContext _context, IMediator _mediator)
        : IRequestHandler<CompleteQuizCommand, CompleteQuizResultDTO>
    {
        private const decimal PassingThreshold = 70m; // confirm real business rule

        public async Task<CompleteQuizResultDTO> Handle(CompleteQuizCommand request, CancellationToken cancellationToken)
        {
            // 1. Load quiz, guard against double-completion
            var quiz = await _context.AgentGeneratedQuizzes
                .FirstOrDefaultAsync(q => q.Id == request.Id, cancellationToken);

            if (quiz == null)
                throw new SqlNullValueException("لا يوجد اختبار مطابق لطلبك.");

            if (quiz.CompletedAt != null)
                throw new Exception("هذا الاختبار قد انتهى بالفعل.");

            // 2. Aggregate answers
            var answers = await _context.StudentQuizAnswers
                .Where(a => a.AgentGeneratedQuizId == quiz.Id)
                .ToListAsync(cancellationToken);

            if (!answers.Any())
                throw new Exception("لا يمكن إنهاء الاختبار قبل الإجابة على أي سؤال.");

            var correctCount = answers.Count(a => a.IsCorrect);
            var scorePercent = Math.Round((decimal)correctCount / answers.Count * 100, 2);
            var isPassed = scorePercent >= PassingThreshold;

            // 3. Finalize the quiz row (still owned directly by this handler via _context)
            quiz.CompletedAt = DateTime.UtcNow;
            quiz.ScorePercent = scorePercent;
            quiz.IsPassed = isPassed;
            quiz.TotalTimeSeconds = (int)(quiz.CompletedAt.Value - quiz.StartedAt).TotalSeconds;

            await _context.SaveChangesAsync(cancellationToken);

            // 4. Side effects — delegated to their own commands, not inlined
            var bonusXp = isPassed ? (int)(scorePercent / 10) : 0;

            var xpResult = await _mediator.Send(new AwardXpCommand(
                StudentProfileId: quiz.StudentProfileId,
                EventType: XpEventType.MicroQuizCorrect, // see note below — replace magic string
                BaseXp: 10,
                BonusXp: bonusXp,
                ReferenceId: quiz.Id,
                ReferenceType: nameof(AgentGeneratedQuiz)
            ), cancellationToken);

            // AwardXpCommandHandler already chains CheckAndAwardBadgesCommand internally,
            // so we do NOT call it again here — avoid double-checking eligibility.

            await UpdateLessonStateAsync(quiz, isPassed, scorePercent, cancellationToken);

            return new CompleteQuizResultDTO
            {
                Id = quiz.Id,
                ScorePercent = scorePercent,
                IsPassed = isPassed,
                TotalTimeSeconds = quiz.TotalTimeSeconds,
                TotalXpAwarded = xpResult.TotalXpAwarded,
                CurrentStreak = 0, // see note below — streak no longer owned by this handler
                NewlyEarnedBadgeIds = new() // badges awarded async via AwardXp's internal chain; see note
            };
        }

        private async Task UpdateLessonStateAsync(AgentGeneratedQuiz quiz, bool isPassed, decimal scorePercent, CancellationToken cancellationToken)
        {
            if (quiz.StandardLessonId == null)
                return;

            var state = await _context.StudentLessonStates
                .FirstOrDefaultAsync(s => s.StudentProfileId == quiz.StudentProfileId
                                        && s.StandardLessonId == quiz.StandardLessonId, cancellationToken);

            if (state == null)
            {
                state = new StudentLessonState
                {
                    Id = Guid.NewGuid(),
                    StudentProfileId = quiz.StudentProfileId,
                    StandardLessonId = quiz.StandardLessonId,
                    FirstStartedAt = quiz.StartedAt
                };
                _context.StudentLessonStates.Add(state);
            }

            state.TotalAttempts += 1;
            state.LastAttemptedAt = DateTime.UtcNow;
            state.AverageQuizScore = state.TotalAttempts == 1
                ? scorePercent
                : Math.Round(((state.AverageQuizScore * (state.TotalAttempts - 1)) + scorePercent) / state.TotalAttempts, 2);

            if (isPassed && state.MasteredAt == null)
            {
                state.MasteredAt = DateTime.UtcNow;
                state.Status = LessonState.Mastered;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}