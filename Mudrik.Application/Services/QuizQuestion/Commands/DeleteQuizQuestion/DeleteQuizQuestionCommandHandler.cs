using MediatR;
using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.QuizQuestion.Commands.DeleteQuizQuestion
{
    public class DeleteQuizQuestionCommandHandler(IAppDbContext _context) : IRequestHandler<DeleteQuizQuestionCommand, bool>
    {
        public async Task<bool> Handle(DeleteQuizQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _context.QuizQuestions.FindAsync(request.id, cancellationToken);

            if (question == null)
                return false;

            var relatedAnswers = await _context.StudentQuizAnswers.Where(s => s.QuizQuestionId == request.id).ToListAsync(cancellationToken);


            if (relatedAnswers.Any())
                _context.StudentQuizAnswers.RemoveRange(relatedAnswers);

            _context.QuizQuestions.Remove(question);

            await _context.SaveChangesAsync(cancellationToken);

            return true;

        }
    }
}
