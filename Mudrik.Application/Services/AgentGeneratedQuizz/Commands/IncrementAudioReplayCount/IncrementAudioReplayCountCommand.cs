using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.AgentGeneratedQuizz.Commands.IncrementAudioReplayCount
{
    public record IncrementAudioReplayCountCommand(Guid id) : IRequest<int>;
}
