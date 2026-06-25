using MediatR;
using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.AgentGeneratedQuizz.DTOs;
using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.AgentGeneratedQuizz.Commands.Generatequiz
{
    public class GenerateQuizCommandHandler(IAppDbContext _context) : IRequestHandler<GenerateQuizCommand, GenerateQuizDTO>
    {
        public async Task<GenerateQuizDTO> Handle(GenerateQuizCommand request, CancellationToken cancellationToken)
        {
            var quiz = new AgentGeneratedQuiz()
            {
                Id = Guid.NewGuid(),
                StudentProfileId = request.StudentProfileId,
                LessonMicroChunkId = request.LessonMicroChunkId,
                StandardLessonId = request.StandardLessonId,
                TotalTimeSeconds = request.TotalTimeSeconds,
                AttemptNumber = 1,
                AudioReplayCount = 0,
                ScorePercent = 0,
                IsPassed = false,
                StartedAt = DateTime.UtcNow,
            };

            await _context.AgentGeneratedQuizzes.AddAsync(quiz, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var quizDTO = new GenerateQuizDTO()
            {
                id = quiz.Id,
                questions = await _context.QuizQuestions.Where(s => s.StandardLessonId == request.StandardLessonId && s.ConceptTag == request.ConceptTag && s.GradeLevel == request.GradeLevel && s.SubjectId == request.SubjectId).Select(s => new QuizQuestionWithoutCorrectOptionId()
                {
                    Id = s.Id,
                    StandardLessonId = s.StandardLessonId,
                    SubjectId = s.SubjectId,
                    QuestionText = s.QuestionText,
                    Format = s.Format,
                    OptionsJson = s.OptionsJson,
                    ConceptTag = s.ConceptTag,
                    GradeLevel = s.GradeLevel,
                    GeneratedAt = s.GeneratedAt

                }).ToListAsync(cancellationToken)

            };

            return quizDTO;
        }
    }
}
