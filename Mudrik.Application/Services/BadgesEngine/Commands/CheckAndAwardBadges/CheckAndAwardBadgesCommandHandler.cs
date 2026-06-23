using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.BadgesEngine.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Badges.Commands.CheckAndAwardBadges
{
    public class CheckAndAwardBadgesCommandHandler(
        IBadgeEligibilityService eligibilityService,
        IBadgeRepository badgeRepository,
        IGamificationNotifier notifier)
        : IRequestHandler<CheckAndAwardBadgesCommand, BadgeAwardResultDto>
    {
        public async Task<BadgeAwardResultDto> Handle(
            CheckAndAwardBadgesCommand request, CancellationToken cancellationToken)
        {
            // Step 1: Evaluate only — no writes here.
            var eligibleBadgeIds = await eligibilityService.EvaluateAsync(
                request.StudentProfileId, cancellationToken);

            var newlyAwarded = new List<EarnedBadgeDto>();

            foreach (var badgeId in eligibleBadgeIds)
            {
                // Step 2: AwardBadgeAsync is idempotent — skips if already exists.
                var studentBadge = await badgeRepository.AwardBadgeAsync(
                    request.StudentProfileId, badgeId, cancellationToken);

                if (studentBadge is null) continue; // already existed, was skipped

                var badge = await badgeRepository.GetByIdAsync(badgeId, cancellationToken);
                if (badge is null) continue;

                newlyAwarded.Add(new EarnedBadgeDto(
                    BadgeId: badge.Id,
                    Name: badge.Name,
                    Description: badge.Description,
                    Rarity: badge.Rarity,
                    ImageUrl: badge.ImageUrl,
                    HasBeenDisplayed: false,
                    EarnedAt: studentBadge.EarnedAt
                ));

                // Step 3: Realtime push — OnBadgeUnlocked
                await notifier.NotifyBadgeUnlockedAsync(
                    request.StudentProfileId, badge.Id, badge.Name);
            }

            return new BadgeAwardResultDto(newlyAwarded);
        }
    }
}
