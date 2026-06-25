using MediatR;
using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.StudentQuizAnswer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mudrik.Application.Services.StudentQuizAnswer.Queries.GetQuestionAnswersByConceptTag
{
    public class GetQuestionAnswersByConceptTagQueryHandler(IAppDbContext _context) : IRequestHandler<GetQuestionAnswersByConceptTagQuery, List<QuizAnswerDTO>>
    {
        public async Task<List<QuizAnswerDTO>> Handle(GetQuestionAnswersByConceptTagQuery request, CancellationToken cancellationToken)
        {
            var answers = await _context.StudentQuizAnswers
                .Where(x => x.QuizQuestion.ConceptTag == request.ConceptTag).ToListAsync(cancellationToken);

            return answers.Select(a =>
            new QuizAnswerDTO(a.Id, a.AgentGeneratedQuizId, a.StudentProfileId, a.QuizQuestionId, a.SelectedOptionId, a.IsCorrect, a.TimeToAnswerMs, a.AnswerChangeCount, a.AnsweredAt
            )).ToList();
        }
    }
}
