using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Interfaces
{
    /// <summary>
    /// Evaluates badge eligibility for a student without writing anything.
    /// Separation of evaluation and award makes this independently testable.
    /// Called by CheckAndAwardBadgesCommandHandler after every XP award.
    /// </summary>
    public interface IBadgeEligibilityService
    {
        /// <summary>
        /// Returns the IDs of badges the student is now eligible for but hasn't earned yet.
        /// Checks all active badges not already in StudentBadges for this student.
        /// </summary>
        Task<IReadOnlyList<Guid>> EvaluateAsync(Guid studentProfileId, CancellationToken cancellationToken);
    }
}
