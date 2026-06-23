using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using Mudrik.Domain.Models;
using Mudrik.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Infrastructure.Repositories
{
    public class BadgeRepository(AppDbContext context) : IBadgeRepository
    {
        public async Task<IReadOnlyList<Badge>> GetAllActiveAsync(CancellationToken cancellationToken)
        {
            return await context.Badges
                .AsNoTracking()
                .Where(b => b.IsActive)
                .OrderBy(b => b.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<Badge?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await context.Badges
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<StudentBadge>> GetEarnedByStudentAsync(
            Guid studentProfileId, CancellationToken cancellationToken)
        {
            return await context.StudentBadges
                .AsNoTracking()
                .Include(sb => sb.Badge)
                .Where(sb => sb.StudentProfileId == studentProfileId)
                .OrderByDescending(sb => sb.EarnedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Badge>> GetUnearnedActiveForStudentAsync(
            Guid studentProfileId, CancellationToken cancellationToken)
        {
            var earnedBadgeIds = await context.StudentBadges
                .AsNoTracking()
                .Where(sb => sb.StudentProfileId == studentProfileId)
                .Select(sb => sb.BadgeId)
                .ToListAsync(cancellationToken);

            return await context.Badges
                .AsNoTracking()
                .Where(b => b.IsActive && !earnedBadgeIds.Contains(b.Id))
                .ToListAsync(cancellationToken);
        }

        public async Task<StudentBadge?> AwardBadgeAsync(
            Guid studentProfileId, Guid badgeId, CancellationToken cancellationToken)
        {
            // Idempotency check — silently skip if already exists.
            var existing = await context.StudentBadges
                .FirstOrDefaultAsync(sb =>
                    sb.StudentProfileId == studentProfileId &&
                    sb.BadgeId == badgeId,
                    cancellationToken);

            if (existing is not null) return null;

            var studentBadge = StudentBadge.Create(studentProfileId, badgeId);
            await context.StudentBadges.AddAsync(studentBadge, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return studentBadge;
        }

        public async Task<StudentBadge?> GetStudentBadgeAsync(
            Guid studentProfileId, Guid badgeId, CancellationToken cancellationToken)
        {
            return await context.StudentBadges
                .FirstOrDefaultAsync(sb =>
                    sb.StudentProfileId == studentProfileId &&
                    sb.BadgeId == badgeId,
                    cancellationToken);
        }

        public async Task MarkDisplayedAsync(
            Guid studentProfileId, Guid badgeId, CancellationToken cancellationToken)
        {
            var studentBadge = await context.StudentBadges
                .FirstOrDefaultAsync(sb =>
                    sb.StudentProfileId == studentProfileId &&
                    sb.BadgeId == badgeId,
                    cancellationToken);

            if (studentBadge is null) return;

            studentBadge.MarkAsDisplayed();
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
