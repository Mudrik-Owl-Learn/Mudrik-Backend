using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Interfaces
{
    public interface IBadgeRepository
    {
        // ---- Badge definitions ----
        Task<IReadOnlyList<Badge>> GetAllActiveAsync(CancellationToken cancellationToken);
        Task<Badge?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        // ---- StudentBadges ----
        Task<IReadOnlyList<StudentBadge>> GetEarnedByStudentAsync(Guid studentProfileId, CancellationToken cancellationToken);

        /// <summary>Returns badges the student has NOT earned yet (active only).</summary>
        Task<IReadOnlyList<Badge>> GetUnearnedActiveForStudentAsync(Guid studentProfileId, CancellationToken cancellationToken);

        /// <summary>Idempotent — silently skips if (studentProfileId, badgeId) already exists.</summary>
        Task<StudentBadge?> AwardBadgeAsync(Guid studentProfileId, Guid badgeId, CancellationToken cancellationToken);

        Task<StudentBadge?> GetStudentBadgeAsync(Guid studentProfileId, Guid badgeId, CancellationToken cancellationToken);

        Task MarkDisplayedAsync(Guid studentProfileId, Guid badgeId, CancellationToken cancellationToken);
    }
}
