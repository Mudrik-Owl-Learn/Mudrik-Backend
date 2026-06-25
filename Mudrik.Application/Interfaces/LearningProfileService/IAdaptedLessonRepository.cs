using Mudrik.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Interfaces
{
    public interface IAdaptedLessonRepository
    {
        Task<AdaptedLesson?> GetByStudentAndLessonAsync(
            Guid studentProfileId, Guid standardLessonId, CancellationToken cancellationToken);

        Task<AdaptedLesson?> GetByIdWithChunksAsync(Guid id, CancellationToken cancellationToken);

        Task<AdaptedLesson> AddAsync(AdaptedLesson lesson, CancellationToken cancellationToken);

        Task AddChunksAsync(LessonMicroChunk[] chunks, CancellationToken cancellationToken);

        Task<StandardLesson?> GetStandardLessonAsync(Guid standardLessonId, CancellationToken cancellationToken);
    }
}
