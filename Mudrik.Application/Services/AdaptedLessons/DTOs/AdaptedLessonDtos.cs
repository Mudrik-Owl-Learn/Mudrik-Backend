using System;
using System.Collections.Generic;

namespace Mudrik.Application.Services.Lessons.DTOs
{
    public record LessonMicroChunkDto(
        Guid Id,
        int ChunkOrder,
        string Format,
        string Title,
        string ContentText,
        string? AudioScriptUrl,
        string? IllustrationUrl,
        int EstimatedDurationSeconds,
        bool IsCompleted,
        DateTime? CompletedAt
    );

    public record AdaptedLessonDto(
        Guid Id,
        Guid StudentProfileId,
        Guid StandardLessonId,
        string AdaptationType,
        int AdaptationVersion,
        int TotalChunks,
        bool PassedSafetyFilter,
        DateTime GeneratedAt,
        DateTime? ExpiresAt,
        IReadOnlyList<LessonMicroChunkDto> Chunks
    );

    /// <summary>Intermediate model parsed from GPT-4o JSON output before persisting.</summary>
    public record GeneratedChunkDto(
        int ChunkOrder,
        string Format,
        string Title,
        string ContentText,
        string? AudioScriptUrl,
        string? IllustrationUrl,
        int EstimatedDurationSeconds
    );
}
