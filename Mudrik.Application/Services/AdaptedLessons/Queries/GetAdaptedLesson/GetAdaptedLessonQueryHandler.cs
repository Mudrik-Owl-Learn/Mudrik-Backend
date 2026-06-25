using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Lessons.Commands.CreateAdaptedLesson;
using Mudrik.Application.Services.Lessons.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Lessons.Queries.GetAdaptedLesson
{
    public class GetAdaptedLessonQueryHandler(
        IAdaptedLessonRepository lessonRepository,
        IMediator mediator)
        : IRequestHandler<GetAdaptedLessonQuery, AdaptedLessonDto>
    {
        public async Task<AdaptedLessonDto> Handle(
            GetAdaptedLessonQuery request, CancellationToken cancellationToken)
        {
            var existing = await lessonRepository.GetByStudentAndLessonAsync(
                request.StudentProfileId, request.StandardLessonId, cancellationToken);

            if (existing is not null)
            {
                var full = await lessonRepository.GetByIdWithChunksAsync(existing.Id, cancellationToken);

                return new AdaptedLessonDto(
                    full!.Id,
                    full.StudentProfileId,
                    full.StandardLessonId,
                    full.AdaptationType,
                    full.AdaptationVersion,
                    full.TotalChunks,
                    full.PassedSafetyFilter,
                    full.GeneratedAt,
                    full.ExpiresAt,
                    full.LessonMicroChunks is null
                        ? new List<LessonMicroChunkDto>()
                        : full.LessonMicroChunks
                            .OrderBy(c => c.ChunkOrder)
                            .Select(c => new LessonMicroChunkDto(
                                c.Id, c.ChunkOrder, c.Format, c.Title,
                                c.ContentText, c.AudioScriptUrl, c.IllustrationUrl,
                                c.EstimatedDurationSeconds, c.IsCompleted, c.CompletedAt))
                            .ToList()
                );
            }

            // No adapted lesson exists yet — trigger generation automatically
            // rather than returning 404. The student just opened a lesson for
            // the first time, so we generate on-demand and return the result.
            return await mediator.Send(
                new CreateAdaptedLessonCommand(request.StudentProfileId, request.StandardLessonId),
                cancellationToken);
        }
    }
}
