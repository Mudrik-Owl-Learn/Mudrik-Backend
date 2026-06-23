using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.BadgesEngine.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Badges.Queries.GetAllBadges
{
    public class GetAllBadgesQueryHandler(IBadgeRepository badgeRepository)
        : IRequestHandler<GetAllBadgesQuery, IReadOnlyList<BadgeDto>>
    {
        public async Task<IReadOnlyList<BadgeDto>> Handle(
            GetAllBadgesQuery request, CancellationToken cancellationToken)
        {
            var badges = await badgeRepository.GetAllActiveAsync(cancellationToken);

            return badges.Select(b => new BadgeDto(
                b.Id,
                b.Name,
                b.Description,
                b.Rarity,
                b.ImageUrl,
                b.EligibilityCriteriaJson,
                b.IsActive
            )).ToList();
        }
    }
}
