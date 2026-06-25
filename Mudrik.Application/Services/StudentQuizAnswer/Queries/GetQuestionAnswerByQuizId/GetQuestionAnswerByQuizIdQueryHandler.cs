using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.StudentQuizAnswer.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentQuizAnswer.Queries.GetQuestionAnswerByQuizId
{
    public class GetQuestionAnswerByQuizIdQueryHandler(IAppDbContext _context) : IRequestHandler<GetQuestionAnswerByQuizIdQuery, List<QuizAnswerDTO>>
    {
        public async Task<List<QuizAnswerDTO>> Handle(GetQuestionAnswerByQuizIdQuery request, CancellationToken cancellationToken)
        {
            var answers = await _context.StudentQuizAnswers
                .Where(x => x.AgentGeneratedQuizId == request.QuizId)
                .ToListAsync(cancellationToken);

            return answers.Select(a =>
            new QuizAnswerDTO(a.Id, a.AgentGeneratedQuizId, a.StudentProfileId, a.QuizQuestionId, a.SelectedOptionId, a.IsCorrect, a.TimeToAnswerMs, a.AnswerChangeCount, a.AnsweredAt
            )).ToList();
        }
    }
}
