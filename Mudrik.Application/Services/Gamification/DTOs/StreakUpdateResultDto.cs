using System;

namespace Mudrik.Application.Services.Gamification.DTOs
{
    public enum StreakUpdateOutcome
    {
        Started,
        Continued,
        AlreadyRecordedToday,
        BrokenAndRestarted,
        SavedByFreeze
    }

    public record StreakUpdateResultDto(
        Guid StudentProfileId,
        StreakUpdateOutcome Outcome,
        int CurrentStreak,
        int LongestStreak,
        int FreezeTokensAvailable,
        bool IsNewLongestStreak
    );
}
