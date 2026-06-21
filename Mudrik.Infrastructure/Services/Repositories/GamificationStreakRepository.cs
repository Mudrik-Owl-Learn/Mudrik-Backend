using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using Mudrik.Domain.Entities;
using Mudrik.Domain.Models;
using Mudrik.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Infrastructure.Services.Repositories
{
    public class GamificationStreakRepository(AppDbContext context) : IGamificationStreakRepository
    {
        private readonly AppDbContext context = context;

        public async Task<GamificationStreak?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await context.GamificationStreaks
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<GamificationStreak?> GetByStudentProfileIdAsync(Guid studentProfileId, CancellationToken cancellationToken)
        {
            // Tracked (not AsNoTracking) on purpose: callers mutate the entity
            // (ApplyXp / ContinueStreak / etc.) then call UpdateAsync in the same unit of work.
            return await context.GamificationStreaks
                .FirstOrDefaultAsync(s => s.StudentProfileId == studentProfileId, cancellationToken);
        }

        public async Task<GamificationStreak> AddAsync(GamificationStreak streak, CancellationToken cancellationToken)
        {
            await context.GamificationStreaks.AddAsync(streak, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return streak;
        }

        public async Task UpdateAsync(GamificationStreak streak, CancellationToken cancellationToken)
        {
            context.GamificationStreaks.Update(streak);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<GamificationStreak>> GetTopByPointsAsync(
            int top, int? gradeLevel, CancellationToken cancellationToken)
        {
            if (gradeLevel.HasValue)
            {
                return await context.GamificationStreaks
                    .AsNoTracking()
                    .Join(
                        context.StudentProfiles.AsNoTracking(),
                        streak => streak.StudentProfileId,
                        student => student.Id,
                        (streak, student) => new { streak, student.GradeLevel })
                    .Where(x => x.GradeLevel == gradeLevel.Value)
                    .OrderByDescending(x => x.streak.TotalPoints)
                    .Take(top)
                    .Select(x => x.streak)
                    .ToListAsync(cancellationToken);
            }

            return await context.GamificationStreaks
                .AsNoTracking()
                .OrderByDescending(s => s.TotalPoints)
                .Take(top)
                .ToListAsync(cancellationToken);
        }
    }
}
