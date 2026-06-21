using MediatR;
using Mudrik.Application.Services.Gamification.DTOs;

namespace Mudrik.Application.Services.Gamification.Commands.GrantFreezeToken
{
    public record GrantFreezeTokenCommand(Guid StudentProfileId, string Reason) : IRequest<GamificationStreakDto>;
}
