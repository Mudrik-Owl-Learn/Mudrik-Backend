using System;
using System.Collections.Generic;

namespace Mudrik.Domain.Entities
{
    public class AdaptedLesson
    {
        public int Id { get; set; }
        public int StudentProfileId { get; set; }
        public int StandardLessonId { get; set; }
        public string AdaptationType { get; set; }
        public int AdaptationVersion { get; set; }
        public int TotalChunks { get; set; }
        public string AdaptedContentJson { get; set; }
        public bool PassedSafetyFilter { get; set; }
        public string SafetyFlagReason { get; set; }
        public bool IsActive { get; set; }
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(30);

        // Navigation properties
        public StudentProfile? StudentProfile { get; set; }
        public StandardLesson? StandardLesson { get; set; }
        public ICollection<LessonMicroChunk> LessonMicroChunks { get; set; } = new List<LessonMicroChunk>();
    }
}
