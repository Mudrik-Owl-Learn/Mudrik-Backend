using System;
using System.Collections.Generic;

namespace Mudrik.Domain.Models
{
    public class LessonMicroChunk
    {
        /// <summary>
        /// One row per chunk produced by the AI generator.
        /// Format is one of: story-chunk | visual-explainer | audio-chunk | standard-chunk
        /// </summary>
        public Guid Id { get; private set; }
        public Guid AdaptedLessonId { get; private set; }
        public Guid StudentProfileId { get; private set; }
        public int ChunkOrder { get; private set; }
        public string Format { get; private set; }
        public string Title { get; private set; }
        public string ContentText { get; private set; }
        public string AudioScriptUrl { get; private set; }
        public string IllustrationUrl { get; private set; }
        public int EstimatedDurationSeconds { get; private set; }
        public bool IsCompleted { get; private set; }
        public DateTime? CompletedAt { get; private set; }

        // Navigation properties
        public AdaptedLesson AdaptedLesson { get; private set; }
        public StudentProfile StudentProfile { get; private set; }
        public ICollection<AgentGeneratedQuiz> AgentGeneratedQuizzes { get; private set; } = new List<AgentGeneratedQuiz>();

        private LessonMicroChunk() { } // EF Core

        public static LessonMicroChunk Create(
            Guid adaptedLessonId,
            Guid studentProfileId,
            int chunkOrder,
            string format,
            string title,
            string contentText,
            string? audioScriptUrl,
            string? illustrationUrl,
            int estimatedDurationSeconds)
        {
            return new LessonMicroChunk
            {
                Id = Guid.NewGuid(),
                AdaptedLessonId = adaptedLessonId,
                StudentProfileId = studentProfileId,
                ChunkOrder = chunkOrder,
                Format = format,
                Title = title,
                ContentText = contentText,
                AudioScriptUrl = audioScriptUrl,
                IllustrationUrl = illustrationUrl,
                EstimatedDurationSeconds = estimatedDurationSeconds,
                IsCompleted = false,
                CompletedAt = null
            };
        }

        public void MarkCompleted()
        {
            IsCompleted = true;
            CompletedAt = DateTime.UtcNow;
        }
    }
}
