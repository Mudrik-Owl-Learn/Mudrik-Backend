using MediatR;
using Mudrik.Application.Services.BadgesEngine.DTOs;
using System.Collections.Generic;

namespace Mudrik.Application.Services.Badges.Queries.GetAllBadges
{
    public record GetAllBadgesQuery : IRequest<IReadOnlyList<BadgeDto>>;
}
