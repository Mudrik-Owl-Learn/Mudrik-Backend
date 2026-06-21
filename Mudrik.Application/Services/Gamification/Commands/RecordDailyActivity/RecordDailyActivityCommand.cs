using MediatR;
using Mudrik.Application.Services.Gamification.DTOs;

namespace Mudrik.Application.Services.Gamification.Commands.RecordDailyActivity
{
    public record RecordDailyActivityCommand(Guid StudentProfileId) : IRequest<StreakUpdateResultDto>;
}
