using System;

namespace Mudrik.Application.Services.Gamification.DTOs
{
    public record XpAwardResultDto(
        Guid StudentProfileId,
        Guid GamificationStreakId,
        string EventType,
        int BaseXp,
        int BonusXp,
        decimal StreakMultiplier,
        int TotalXpAwarded,
        int TotalPoints,
        int PreviousLevel,
        int CurrentLevel,
        bool LeveledUp
    );
}
