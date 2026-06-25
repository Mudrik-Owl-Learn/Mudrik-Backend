using MediatR;
using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.StudentQuizAnswer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentQuizAnswer.Queries.GetQuestionAnswersByQuestionId
{
    public class GetQuestionAnswersByQuestionIdQueryHandler(IAppDbContext _context) : IRequestHandler<GetQuestionAnswersByQuestionIdQuery, List<QuizAnswerDTO>>
    {
        public async Task<List<QuizAnswerDTO>> Handle(GetQuestionAnswersByQuestionIdQuery request, CancellationToken cancellationToken)
        {
            var answers = await _context.StudentQuizAnswers
               .Where(x => x.QuizQuestionId == request.QuestionId)
               .ToListAsync(cancellationToken);

            return answers.Select(a =>
            new QuizAnswerDTO(a.Id, a.AgentGeneratedQuizId, a.StudentProfileId, a.QuizQuestionId, a.SelectedOptionId, a.IsCorrect, a.TimeToAnswerMs, a.AnswerChangeCount, a.AnsweredAt
            )).ToList();
        }
    }
}
