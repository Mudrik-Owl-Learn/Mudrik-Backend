using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.BadgesEngine.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Badges.Queries.GetStudentBadges
{
    public class GetStudentBadgesQueryHandler(IBadgeRepository badgeRepository)
        : IRequestHandler<GetStudentBadgesQuery, StudentBadgesDto>
    {
        public async Task<StudentBadgesDto> Handle(
            GetStudentBadgesQuery request, CancellationToken cancellationToken)
        {
            var earned = await badgeRepository.GetEarnedByStudentAsync(
                request.StudentProfileId, cancellationToken);

            var locked = await badgeRepository.GetUnearnedActiveForStudentAsync(
                request.StudentProfileId, cancellationToken);

            var earnedDtos = earned.Select(sb => new EarnedBadgeDto(
                BadgeId: sb.BadgeId,
                Name: sb.Badge.Name,
                Description: sb.Badge.Description,
                Rarity: sb.Badge.Rarity,
                ImageUrl: sb.Badge.ImageUrl,
                HasBeenDisplayed: sb.HasBeenDisplayed,
                EarnedAt: sb.EarnedAt
            )).ToList();

            // Locked badges show silhouette only — no description to avoid spoilers.
            var lockedDtos = locked.Select(b => new LockedBadgeDto(
                BadgeId: b.Id,
                Name: b.Name,
                SilhouetteImageUrl: BuildSilhouetteUrl(b.ImageUrl)
            )).ToList();

            return new StudentBadgesDto(
                StudentProfileId: request.StudentProfileId,
                Earned: earnedDtos,
                Locked: lockedDtos
            );
        }

        /// <summary>
        /// Derives silhouette URL from the badge's original image URL.
        /// Convention: replace the filename with "silhouette_{filename}".
        /// Adjust this logic to match the actual CDN/storage structure.
        /// </summary>
        private static string BuildSilhouetteUrl(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return string.Empty;

            var lastSlash = imageUrl.LastIndexOf('/');
            if (lastSlash < 0) return $"silhouette_{imageUrl}";

            var folder = imageUrl[..(lastSlash + 1)];
            var filename = imageUrl[(lastSlash + 1)..];
            return $"{folder}silhouette_{filename}";
        }
    }
}
