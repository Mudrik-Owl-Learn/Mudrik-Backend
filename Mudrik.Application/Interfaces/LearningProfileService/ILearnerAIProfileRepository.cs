using Mudrik.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Interfaces
{
    public interface ILearnerAIProfileRepository
    {
        Task<LearnerAIProfile?> GetByStudentProfileIdAsync(Guid studentProfileId, CancellationToken cancellationToken);
        Task<LearnerAIProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<LearnerAIProfile> AddAsync(LearnerAIProfile profile, CancellationToken cancellationToken);
        Task UpdateAsync(LearnerAIProfile profile, CancellationToken cancellationToken);
    }
}
