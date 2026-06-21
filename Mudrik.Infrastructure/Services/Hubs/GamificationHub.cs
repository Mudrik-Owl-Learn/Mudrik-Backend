using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Mudrik.Infrastructure.Services.Hubs
{
    /// <summary>
    /// Realtime hub for gamification feedback (XP, levels, streaks, badges).
    /// Each connected client joins group "student:{studentProfileId}" so the
    /// server can target pushes to exactly one learner's open sessions
    /// (e.g. same child on tablet + phone, or a parent observing via Child Switcher).
    ///
    /// Route: /hubs/gamification
    /// Client events (server -> client), matching Project-context.md:
    ///   OnPointsEarned(XpAwardResultDto)
    ///   OnBadgeUnlocked(BadgeUnlockedPayload)
    ///   OnStreakUpdated(StreakUpdateResultDto)
    ///   OnLevelUp(LevelUpPayload)
    /// </summary>
    [Authorize]
    public class GamificationHub : Hub
    {
        private static string StudentGroupName(Guid studentProfileId) => $"student:{studentProfileId}";

        public override async Task OnConnectedAsync()
        {
            var studentProfileId = GetStudentProfileIdFromContext();
            if (studentProfileId.HasValue)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, StudentGroupName(studentProfileId.Value));
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var studentProfileId = GetStudentProfileIdFromContext();
            if (studentProfileId.HasValue)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, StudentGroupName(studentProfileId.Value));
            }

            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>Used by a parent session to observe a specific child's live updates from /parent/dashboard.</summary>
        public async Task SubscribeToStudent(Guid studentProfileId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, StudentGroupName(studentProfileId));
        }

        public async Task UnsubscribeFromStudent(Guid studentProfileId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, StudentGroupName(studentProfileId));
        }

        private Guid? GetStudentProfileIdFromContext()
        {
            var claim = Context.User?.FindFirst("studentProfileId");
            return claim is not null && Guid.TryParse(claim.Value, out var id) ? id : null;
        }

        internal static string GroupNameFor(Guid studentProfileId) => StudentGroupName(studentProfileId);
    }
}
