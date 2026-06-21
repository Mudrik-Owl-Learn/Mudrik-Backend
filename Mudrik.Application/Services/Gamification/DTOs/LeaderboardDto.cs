using System.Collections.Generic;

namespace Mudrik.Application.Services.Gamification.DTOs
{
    public record LeaderboardEntryDto(
        int Rank,
        Guid StudentProfileId,
        string DisplayName,
        string AvatarId,
        int TotalPoints,
        int CurrentLevel
    );

    public record LeaderboardDto(IReadOnlyList<LeaderboardEntryDto> Entries);
}
