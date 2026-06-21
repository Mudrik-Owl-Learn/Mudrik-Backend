using Microsoft.AspNetCore.SignalR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Gamification.DTOs;
using Mudrik.Infrastructure.Services.Hubs;
using System.Threading.Tasks;

namespace Mudrik.Infrastructure.Realtime
{
    /// <summary>
    /// Infrastructure-side implementation of IGamificationNotifier.
    /// Registered in DI as: services.AddScoped&lt;IGamificationNotifier, GamificationNotifier&gt;();
    /// </summary>
    public class GamificationNotifier(IHubContext<GamificationHub> hubContext) : IGamificationNotifier
    {
        public Task NotifyPointsEarnedAsync(Guid studentProfileId, XpAwardResultDto result)
        {
            return hubContext.Clients
                .Group(GamificationHub.GroupNameFor(studentProfileId))
                .SendAsync("OnPointsEarned", result);
        }

        public Task NotifyLevelUpAsync(Guid studentProfileId, int newLevel)
        {
            return hubContext.Clients
                .Group(GamificationHub.GroupNameFor(studentProfileId))
                .SendAsync("OnLevelUp", new LevelUpPayload(studentProfileId, newLevel));
        }

        public Task NotifyStreakUpdatedAsync(Guid studentProfileId, StreakUpdateResultDto result)
        {
            return hubContext.Clients
                .Group(GamificationHub.GroupNameFor(studentProfileId))
                .SendAsync("OnStreakUpdated", result);
        }

        public Task NotifyBadgeUnlockedAsync(Guid studentProfileId, int badgeId, string badgeName)
        {
            return hubContext.Clients
                .Group(GamificationHub.GroupNameFor(studentProfileId))
                .SendAsync("OnBadgeUnlocked", new BadgeUnlockedPayload(studentProfileId, badgeId, badgeName));
        }
    }
}
