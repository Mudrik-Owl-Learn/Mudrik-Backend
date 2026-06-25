using MediatR;
using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.StudentQuizAnswer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentQuizAnswer.Queries.GetQuestionAnswerByStudentId
{
    public class GetQuestionAnswersByStudentIdQueryHandler(IAppDbContext _context) : IRequestHandler<GetQuestionAnswersByStudentIdQuery, List<QuizAnswerDTO>>
    {
        public async Task<List<QuizAnswerDTO>> Handle(GetQuestionAnswersByStudentIdQuery request, CancellationToken cancellationToken)
        {
            var answers = await _context.StudentQuizAnswers
                .Where(x => x.StudentProfileId == request.StudentId)
                .ToListAsync(cancellationToken);

            return answers.Select(a =>
            new QuizAnswerDTO(a.Id, a.AgentGeneratedQuizId, a.StudentProfileId, a.QuizQuestionId, a.SelectedOptionId, a.IsCorrect, a.TimeToAnswerMs, a.AnswerChangeCount, a.AnsweredAt
            )).ToList();
        }
    }
}
