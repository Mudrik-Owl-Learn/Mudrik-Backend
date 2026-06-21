using MediatR;
using Mudrik.Application.Services.Gamification.DTOs;

namespace Mudrik.Application.Services.Gamification.Queries.GetStreakStatus
{
    public record GetStreakStatusQuery(Guid StudentProfileId) : IRequest<GamificationStreakDto>;
}
