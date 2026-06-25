using MediatR;
using Mudrik.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.AgentGeneratedQuizz.Commands.IncrementAudioReplayCount
{
    public class IncrementAudioReplayCountCommandHandler(IAppDbContext _context) : IRequestHandler<IncrementAudioReplayCountCommand, int>
    {
        public async Task<int> Handle(IncrementAudioReplayCountCommand request, CancellationToken cancellationToken)
        {
            var quiz = await _context.AgentGeneratedQuizzes.FindAsync(request.id, cancellationToken);

            if (quiz == null)
                return 0;

            quiz.AudioReplayCount = quiz.AudioReplayCount + 1;
            await _context.SaveChangesAsync(cancellationToken);

            return quiz.AudioReplayCount;

        }
    }
}
