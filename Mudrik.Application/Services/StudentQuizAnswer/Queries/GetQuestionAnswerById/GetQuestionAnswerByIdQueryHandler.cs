using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.StudentQuizAnswer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentQuizAnswer.Queries.GetQuestionAnswerById
{
    public class GetQuestionAnswerByIdQueryHandler(IAppDbContext _context) : IRequestHandler<GetQuestionAnswerByIdQuery, QuizAnswerDTO>
    {
        public async Task<QuizAnswerDTO> Handle(GetQuestionAnswerByIdQuery request, CancellationToken cancellationToken)
        {
            var answer = await _context.StudentQuizAnswers.FindAsync(request.id, cancellationToken);

            if (answer == null)
                return null;


            var answerDto = new QuizAnswerDTO()
            {
                Id = answer.Id,
                AgentGeneratedQuizId = answer.AgentGeneratedQuizId,
                StudentProfileId = answer.StudentProfileId,
                QuizQuestionId = answer.QuizQuestionId,
                SelectedOptionId = answer.SelectedOptionId,
                IsCorrect = answer.IsCorrect,
                TimeToAnswerMs = answer.TimeToAnswerMs,
                AnswerChangeCount = answer.AnswerChangeCount,
                AnsweredAt = answer.AnsweredAt
            };

            return answerDto;
        }
    }
}
