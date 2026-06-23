using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Mudrik.API.Hubs
{
    [Authorize]
    public class GamificationHub : Hub
    {
        private static string StudentGroupName(Guid studentProfileId) => $"student:{studentProfileId}";

        public override async Task OnConnectedAsync()
        {
            var studentProfileId = GetStudentProfileIdFromContext();
            if (studentProfileId.HasValue)
                await Groups.AddToGroupAsync(Context.ConnectionId, StudentGroupName(studentProfileId.Value));

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var studentProfileId = GetStudentProfileIdFromContext();
            if (studentProfileId.HasValue)
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, StudentGroupName(studentProfileId.Value));

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SubscribeToStudent(Guid studentProfileId)
            => await Groups.AddToGroupAsync(Context.ConnectionId, StudentGroupName(studentProfileId));

        public async Task UnsubscribeFromStudent(Guid studentProfileId)
            => await Groups.RemoveFromGroupAsync(Context.ConnectionId, StudentGroupName(studentProfileId));

        private Guid? GetStudentProfileIdFromContext()
        {
            var claim = Context.User?.FindFirst("studentProfileId");
            return claim is not null && Guid.TryParse(claim.Value, out var id) ? id : null;
        }

        internal static string GroupNameFor(Guid studentProfileId) => StudentGroupName(studentProfileId);
    }
}
