using System;

namespace Mudrik.Application.Services.Gamification.DTOs
{
    public record GamificationStreakDto(
        Guid Id,
        Guid StudentProfileId,
        int TotalPoints,
        int CurrentLevel,
        int CurrentStreak,
        int LongestStreak,
        DateOnly LastStreakDate,
        int FreezeTokensAvailable,
        bool IsAtRiskToday,
        DateTime UpdatedAt
    );
}
