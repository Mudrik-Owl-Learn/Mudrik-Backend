using MediatR;
using System;

namespace Mudrik.Application.Services.Badges.Commands.MarkBadgeDisplayed
{
    public record MarkBadgeDisplayedCommand(
        Guid StudentProfileId,
        Guid BadgeId
    ) : IRequest;
}
