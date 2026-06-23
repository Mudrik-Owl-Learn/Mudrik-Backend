using Mudrik.Application.Services.Gamification.DTOs;
using System;
using System.Threading.Tasks;

namespace Mudrik.Application.Interfaces
{
    public record LevelUpPayload(Guid StudentProfileId, int NewLevel);

    public record BadgeUnlockedPayload(Guid StudentProfileId, Guid BadgeId, string BadgeName);

    public interface IGamificationNotifier
    {
        Task NotifyPointsEarnedAsync(Guid studentProfileId, XpAwardResultDto result);
        Task NotifyLevelUpAsync(Guid studentProfileId, int newLevel);
        Task NotifyStreakUpdatedAsync(Guid studentProfileId, StreakUpdateResultDto result);
        Task NotifyBadgeUnlockedAsync(Guid studentProfileId, Guid badgeId, string badgeName);
    }
}
