using MediatR;
using Mudrik.Application.Services.Gamification.DTOs;

namespace Mudrik.Application.Services.Gamification.Queries.GetLeaderboard
{
    public enum LeaderboardScope
    {
        Global,
        ByGradeLevel
    }

    public record GetLeaderboardQuery(
        LeaderboardScope Scope = LeaderboardScope.Global,
        int? GradeLevel = null,
        int Top = 10
    ) : IRequest<LeaderboardDto>;
}
