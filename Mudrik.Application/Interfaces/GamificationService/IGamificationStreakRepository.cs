using Mudrik.Domain.Entities;
using Mudrik.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Interfaces
{
    public interface IGamificationStreakRepository
    {
        Task<GamificationStreak?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<GamificationStreak?> GetByStudentProfileIdAsync(Guid studentProfileId, CancellationToken cancellationToken);

        Task<GamificationStreak> AddAsync(GamificationStreak streak, CancellationToken cancellationToken);

        Task UpdateAsync(GamificationStreak streak, CancellationToken cancellationToken);

        /// <summary>Top N students ordered by TotalPoints descending, optionally filtered by grade level (joined via StudentProfiles).</summary>
        Task<IReadOnlyList<GamificationStreak>> GetTopByPointsAsync(int top, int? gradeLevel, CancellationToken cancellationToken);
    }
}
