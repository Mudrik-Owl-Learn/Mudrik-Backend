using Mudrik.Application.Services.Gamification.DTOs;
using System.Threading.Tasks;

namespace Mudrik.Application.Interfaces
{
    public record LevelUpPayload(Guid StudentProfileId, int NewLevel);

    public record BadgeUnlockedPayload(Guid StudentProfileId, int BadgeId, string BadgeName);

    /// <summary>
    /// Abstraction over the realtime push layer (SignalR GamificationHub).
    /// Application layer depends on this, never on SignalR directly.
    /// </summary>
    public interface IGamificationNotifier
    {
        Task NotifyPointsEarnedAsync(Guid studentProfileId, XpAwardResultDto result);
        Task NotifyLevelUpAsync(Guid studentProfileId, int newLevel);
        Task NotifyStreakUpdatedAsync(Guid studentProfileId, StreakUpdateResultDto result);
        Task NotifyBadgeUnlockedAsync(Guid studentProfileId, int badgeId, string badgeName);
    }
}
