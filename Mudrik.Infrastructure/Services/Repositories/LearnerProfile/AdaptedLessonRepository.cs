using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using Mudrik.Domain.Models;
using Mudrik.Infrastructure.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Infrastructure.Repositories
{
    public class AdaptedLessonRepository(AppDbContext context) : IAdaptedLessonRepository
    {
        public async Task<AdaptedLesson?> GetByStudentAndLessonAsync(
            Guid studentProfileId, Guid standardLessonId, CancellationToken cancellationToken)
            => await context.AdaptedLessons
                .AsNoTracking()
                .FirstOrDefaultAsync(al =>
                    al.StudentProfileId == studentProfileId &&
                    al.StandardLessonId == standardLessonId &&
                    al.IsActive,
                    cancellationToken);

        public async Task<AdaptedLesson?> GetByIdWithChunksAsync(
            Guid id, CancellationToken cancellationToken)
            => await context.AdaptedLessons
                .AsNoTracking()
                .Include(al => al.LessonMicroChunks.OrderBy(c => c.ChunkOrder))
                    .ThenInclude(c => c.StudentProfile)
                .FirstOrDefaultAsync(al => al.Id == id, cancellationToken);

        public async Task<AdaptedLesson> AddAsync(
            AdaptedLesson lesson, CancellationToken cancellationToken)
        {
            await context.AdaptedLessons.AddAsync(lesson, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return lesson;
        }

        public async Task AddChunksAsync(
            LessonMicroChunk[] chunks, CancellationToken cancellationToken)
        {
            await context.LessonMicroChunks.AddRangeAsync(chunks, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<StandardLesson?> GetStandardLessonAsync(
            Guid standardLessonId, CancellationToken cancellationToken)
            => await context.StandardLessons
                .AsNoTracking()
                .FirstOrDefaultAsync(sl => sl.Id == standardLessonId && sl.IsActive, cancellationToken);
    }
}
