using MediatR;
using Mudrik.Application.Services.AgentGeneratedQuizz.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.AgentGeneratedQuizz.Commands.CompleteQuiz
{
    public record CompleteQuizCommand(Guid Id) : IRequest<CompleteQuizResultDTO>;
}
