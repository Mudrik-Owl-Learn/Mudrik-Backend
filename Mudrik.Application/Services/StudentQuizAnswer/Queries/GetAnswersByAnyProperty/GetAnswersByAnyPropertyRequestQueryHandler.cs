using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.StudentQuizAnswer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentQuizAnswer.Queries.GetAnswersByAnyProperty
{
    public class GetAnswersByAnyPropertyRequestQueryHandler(IAppDbContext _context) : IRequestHandler<GetAnswersByAnyPropertyRequestQuery, List<QuizAnswerDTO>>
    {
        public async Task<List<QuizAnswerDTO>> Handle(GetAnswersByAnyPropertyRequestQuery request, CancellationToken cancellationToken)
        {
            var query = _context.StudentQuizAnswers
                .Where(a => a.StudentProfileId == request.StudentProfileId);

            if (!string.IsNullOrWhiteSpace(request.ConceptTag))
                query = query.Where(a => a.QuizQuestion.ConceptTag == request.ConceptTag);

            if (request.StandardLessonId.HasValue)
                query = query.Where(a => a.QuizQuestion.StandardLessonId == request.StandardLessonId.Value);

            if (request.From.HasValue)
                query = query.Where(a => a.AnsweredAt >= request.From.Value);

            if (request.To.HasValue)
                query = query.Where(a => a.AnsweredAt <= request.To.Value);

            if (request.IsCorrect.HasValue)
                query = query.Where(a => a.IsCorrect == request.IsCorrect.Value);

            var totalCount = await query.ToListAsync(cancellationToken);
            return query.Select(a => new QuizAnswerDTO
            {
                Id = a.Id,
                AgentGeneratedQuizId = a.AgentGeneratedQuizId,
                StudentProfileId = a.StudentProfileId,
                QuizQuestionId = a.QuizQuestionId,
                SelectedOptionId = a.SelectedOptionId,
                IsCorrect = a.IsCorrect,
                TimeToAnswerMs = a.TimeToAnswerMs,
                AnswerChangeCount = a.AnswerChangeCount,
                AnsweredAt = a.AnsweredAt
            }).ToList();
        }
    }
}
