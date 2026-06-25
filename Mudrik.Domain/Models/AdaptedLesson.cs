using System;
using System.Collections.Generic;

namespace Mudrik.Domain.Models
{
    public class AdaptedLesson
    {
        public Guid Id { get; set; }
        public Guid StudentProfileId { get; private set; }
        public Guid StandardLessonId { get; private set; }
        public string AdaptationType { get; private set; }
        public int AdaptationVersion { get; private set; }
        public int TotalChunks { get; private set; }
        public string AdaptedContentJson { get; private set; }
        public bool PassedSafetyFilter { get; private set; }
        public string SafetyFlagReason { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime GeneratedAt { get; private set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; private set; } = DateTime.UtcNow.AddDays(30);

        // Navigation properties
        public StudentProfile? StudentProfile { get; private set; }
        public StandardLesson? StandardLesson { get; private set; }
        public ICollection<LessonMicroChunk> LessonMicroChunks { get; private set; } = new List<LessonMicroChunk>();

        private AdaptedLesson() { }

        public static AdaptedLesson Create(
            Guid studentProfileId,
            Guid standardLessonId,
            string adaptationType,
            int totalChunks,
            string adaptedContentJson,
            DateTime expiresAt)
        {
            return new AdaptedLesson
            {
                Id = Guid.NewGuid(),
                StudentProfileId = studentProfileId,
                StandardLessonId = standardLessonId,
                AdaptationType = adaptationType,
                AdaptationVersion = 1,
                TotalChunks = totalChunks,
                AdaptedContentJson = adaptedContentJson,
                PassedSafetyFilter = false, // flipped async on Day 4
                SafetyFlagReason = null,
                IsActive = true,
                GeneratedAt = DateTime.UtcNow,
                ExpiresAt = expiresAt
            };
        }

        public void PassSafetyFilter() => PassedSafetyFilter = true;

        public void FlagSafety(string reason)
        {
            PassedSafetyFilter = false;
            SafetyFlagReason = reason;
        }

        public void Deactivate() => IsActive = false;
    }
}
