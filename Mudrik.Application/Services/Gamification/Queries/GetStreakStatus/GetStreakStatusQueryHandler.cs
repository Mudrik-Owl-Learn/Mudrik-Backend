using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Gamification.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Gamification.Queries.GetStreakStatus
{
    public class GetStreakStatusQueryHandler(IGamificationStreakRepository streakRepository)
        : IRequestHandler<GetStreakStatusQuery, GamificationStreakDto>
    {
        public async Task<GamificationStreakDto> Handle(GetStreakStatusQuery request, CancellationToken cancellationToken)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var streak = await streakRepository.GetByStudentProfileIdAsync(request.StudentProfileId, cancellationToken);

            if (streak is null)
            {
                return new GamificationStreakDto(
                    Id: Guid.Empty,
                    StudentProfileId: request.StudentProfileId,
                    TotalPoints: 0,
                    CurrentLevel: 1,
                    CurrentStreak: 0,
                    LongestStreak: 0,
                    LastStreakDate: today,
                    FreezeTokensAvailable: 0,
                    IsAtRiskToday: false,
                    UpdatedAt: DateTime.UtcNow
                );
            }

            return new GamificationStreakDto(
                streak.Id,
                streak.StudentProfileId,
                streak.TotalPoints,
                streak.CurrentLevel,
                streak.CurrentStreak,
                streak.LongestStreak,
                streak.LastStreakDate,
                streak.FreezeTokensAvailable,
                IsAtRiskToday: streak.LastStreakDate.DayNumber < today.DayNumber,
                streak.UpdatedAt
            );
        }
    }
}
