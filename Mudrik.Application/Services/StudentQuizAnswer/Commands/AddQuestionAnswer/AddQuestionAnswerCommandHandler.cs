using MediatR;
using Mudrik.Application.Interfaces;
using System.Linq;
using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mudrik.Application.Services.StudentQuizAnswer.Commands.AddQuestionAnswer
{
    public class AddQuestionAnswerCommandHandler : IRequestHandler<AddQuestionAnswerCommand, Guid>
    {
        private readonly IAppDbContext _context;

        public AddQuestionAnswerCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(AddQuestionAnswerCommand request, CancellationToken cancellationToken)
        {

            var answer = new Domain.Models.StudentQuizAnswer()
            {
                Id = Guid.NewGuid(),
                AgentGeneratedQuizId = request.AgentGeneratedQuizId,
                StudentProfileId = request.StudentProfileId,
                QuizQuestionId = request.QuizQuestionId,
                SelectedOptionId = request.SelectedOptionId,
                IsCorrect = await _context.QuizQuestions.Where(x => x.Id == request.QuizQuestionId && x.CorrectOptionId == request.SelectedOptionId).AnyAsync(cancellationToken),
                TimeToAnswerMs = request.TimeToAnswerMs,
                AnswerChangeCount = request.AnswerChangeCount,
                AnsweredAt = DateTime.UtcNow
            };

            await _context.StudentQuizAnswers.AddAsync(answer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return answer.Id;

        }
    }
}
