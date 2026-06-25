using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using Mudrik.Domain.Models;
using Mudrik.Infrastructure.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Infrastructure.Repositories
{
    public class LearnerAIProfileRepository(AppDbContext context) : ILearnerAIProfileRepository
    {
        public async Task<LearnerAIProfile?> GetByStudentProfileIdAsync(
            Guid studentProfileId, CancellationToken cancellationToken)
            => await context.LearnerAIProfiles
                .FirstOrDefaultAsync(p => p.StudentProfileId == studentProfileId, cancellationToken);

        public async Task<LearnerAIProfile?> GetByIdAsync(
            Guid id, CancellationToken cancellationToken)
            => await context.LearnerAIProfiles
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task<LearnerAIProfile> AddAsync(
            LearnerAIProfile profile, CancellationToken cancellationToken)
        {
            await context.LearnerAIProfiles.AddAsync(profile, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return profile;
        }

        public async Task UpdateAsync(
            LearnerAIProfile profile, CancellationToken cancellationToken)
        {
            context.LearnerAIProfiles.Update(profile);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
