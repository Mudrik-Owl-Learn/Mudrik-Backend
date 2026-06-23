using MediatR;
using Mudrik.Application.Services.BadgesEngine.DTOs;
using System;

namespace Mudrik.Application.Services.Badges.Commands.CheckAndAwardBadges
{
    public record CheckAndAwardBadgesCommand(Guid StudentProfileId) : IRequest<BadgeAwardResultDto>;
}
