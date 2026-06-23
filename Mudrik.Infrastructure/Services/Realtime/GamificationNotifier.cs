using Microsoft.AspNetCore.SignalR;
using Mudrik.API.Hubs;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Gamification.DTOs;
using System;
using System.Threading.Tasks;

namespace Mudrik.Infrastructure.Realtime
{
    public class GamificationNotifier(IHubContext<GamificationHub> hubContext) : IGamificationNotifier
    {
        public Task NotifyPointsEarnedAsync(Guid studentProfileId, XpAwardResultDto result)
            => hubContext.Clients
                .Group(GamificationHub.GroupNameFor(studentProfileId))
                .SendAsync("OnPointsEarned", result);

        public Task NotifyLevelUpAsync(Guid studentProfileId, int newLevel)
            => hubContext.Clients
                .Group(GamificationHub.GroupNameFor(studentProfileId))
                .SendAsync("OnLevelUp", new LevelUpPayload(studentProfileId, newLevel));

        public Task NotifyStreakUpdatedAsync(Guid studentProfileId, StreakUpdateResultDto result)
            => hubContext.Clients
                .Group(GamificationHub.GroupNameFor(studentProfileId))
                .SendAsync("OnStreakUpdated", result);

        public Task NotifyBadgeUnlockedAsync(Guid studentProfileId, Guid badgeId, string badgeName)
            => hubContext.Clients
                .Group(GamificationHub.GroupNameFor(studentProfileId))
                .SendAsync("OnBadgeUnlocked", new BadgeUnlockedPayload(studentProfileId, badgeId, badgeName));
    }
}
