using MediatR;
using Mudrik.Application.Services.QuizQuestion.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.QuizQuestion.Queries.GetQuizQuestionById
{
    public record GetQuizQuestionByIdQuery(Guid id) : IRequest<GenerateQuizQuestionDTO>;
}
