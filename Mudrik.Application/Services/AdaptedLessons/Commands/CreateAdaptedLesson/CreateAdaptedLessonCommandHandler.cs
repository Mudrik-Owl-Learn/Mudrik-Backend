using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Lessons.DTOs;
using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Lessons.Commands.CreateAdaptedLesson
{
    public class CreateAdaptedLessonCommandHandler(
        IAdaptedLessonRepository lessonRepository,
        ILearnerAIProfileRepository profileRepository,
        IAdaptedLessonGenerator generator)
        : IRequestHandler<CreateAdaptedLessonCommand, AdaptedLessonDto>
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<AdaptedLessonDto> Handle(
            CreateAdaptedLessonCommand request, CancellationToken cancellationToken)
        {
            // 1. Load LearnerAIProfile — must exist before generating.
            var profile = await profileRepository.GetByStudentProfileIdAsync(
                request.StudentProfileId, cancellationToken)
                ?? throw new ValidationException(
                    "LearnerAIProfile not found. Complete the diagnostic quiz first.");

            // 2. Load StandardLesson.
            var standardLesson = await lessonRepository.GetStandardLessonAsync(
                request.StandardLessonId, cancellationToken)
                ?? throw new ValidationException("StandardLesson not found or inactive.");

            // 3. Call AI generator — 30s timeout enforced inside the implementation.
            var adaptedContentJson = await generator.GenerateAsync(
                standardLesson, profile, cancellationToken);

            // 4. Parse generated chunks to determine TotalChunks.
            var generatedChunks = ParseChunks(adaptedContentJson);

            // 5. Persist AdaptedLesson row.
            var adaptationType = DeriveAdaptationType(profile);
            var adaptedLesson = AdaptedLesson.Create(
                request.StudentProfileId,
                request.StandardLessonId,
                adaptationType,
                totalChunks: generatedChunks.Count,
                adaptedContentJson: adaptedContentJson,
                expiresAt: DateTime.UtcNow.AddDays(7)
            );

            await lessonRepository.AddAsync(adaptedLesson, cancellationToken);

            // 6. Seed LessonMicroChunks rows — one per chunk, ordered by ChunkOrder.
            var microChunks = generatedChunks.Select((c, index) =>
                LessonMicroChunk.Create(
                    adaptedLessonId: adaptedLesson.Id,
                    studentProfileId: request.StudentProfileId,
                    chunkOrder: index + 1,
                    format: c.Format,
                    title: c.Title,
                    contentText: c.ContentText,
                    audioScriptUrl: c.AudioScriptUrl,
                    illustrationUrl: c.IllustrationUrl,
                    estimatedDurationSeconds: c.EstimatedDurationSeconds
                )).ToArray();

            await lessonRepository.AddChunksAsync(microChunks, cancellationToken);

            return MapToDto(adaptedLesson, microChunks);
        }

        private static List<GeneratedChunkDto> ParseChunks(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<List<GeneratedChunkDto>>(json, JsonOptions)
                    ?? new List<GeneratedChunkDto>();
            }
            catch
            {
                throw new ValidationException(
                    "AI generator returned malformed JSON. Generation failed.");
            }
        }

        /// <summary>
        /// Derives a human-readable AdaptationType label from the profile
        /// for admin visibility in the curriculum management panel.
        /// </summary>
        private static string DeriveAdaptationType(LearnerAIProfile profile)
        {
            if (profile.AudioSupportRequired) return "AudioNarration";
            if (profile.DyslexiaSeverity >= 60) return "VisualExplainer";
            if (profile.ADHDSeverity >= 60) return "StoryBased";
            return "Standard";
        }

        private static AdaptedLessonDto MapToDto(AdaptedLesson lesson, LessonMicroChunk[] chunks) => new(
            lesson.Id,
            lesson.StudentProfileId,
            lesson.StandardLessonId,
            lesson.AdaptationType,
            lesson.AdaptationVersion,
            lesson.TotalChunks,
            lesson.PassedSafetyFilter,
            lesson.GeneratedAt,
            lesson.ExpiresAt,
            chunks.Select(c => new LessonMicroChunkDto(
                c.Id, c.ChunkOrder, c.Format, c.Title,
                c.ContentText, c.AudioScriptUrl, c.IllustrationUrl,
                c.EstimatedDurationSeconds, c.IsCompleted, c.CompletedAt
            )).ToList()
        );
    }
}
