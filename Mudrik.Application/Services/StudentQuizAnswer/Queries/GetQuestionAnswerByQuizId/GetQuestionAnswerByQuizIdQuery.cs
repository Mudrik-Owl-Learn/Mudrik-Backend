using MediatR;
using Mudrik.Application.Services.StudentQuizAnswer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentQuizAnswer.Queries.GetQuestionAnswerByQuizId
{
    public record GetQuestionAnswerByQuizIdQuery(Guid QuizId) : IRequest<List<QuizAnswerDTO>>;
}
