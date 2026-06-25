using System;

namespace Mudrik.Application.Services.Gamification.DTOs
{
    public record XpTransactionDto(
        Guid Id,
        Guid StudentProfileId,
        Guid GamificationStreakId,
        string EventType,
        int BaseXp,
        int BonusXp,
        decimal StreakMultiplier,
        int TotalXpAwarded,
        Guid? ReferenceId,
        string? ReferenceType,
        DateTime AwardedAt
    );
}
