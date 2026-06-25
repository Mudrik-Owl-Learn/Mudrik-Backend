using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.QuizQuestion.Commands.DeleteQuizQuestion
{
    public record DeleteQuizQuestionCommand(Guid id) : IRequest<bool>;
}
