using Mudrik.Domain.Enums;
using System;

namespace Mudrik.Domain.Entities
{
    public class StudentLessonState
    {
        public Guid Id { get; set; }
        public Guid StudentProfileId { get; set; }
        public Guid StandardLessonId { get; set; }
        public LessonState Status { get; set; }
        public decimal AverageQuizScore { get; set; }
        public int TotalAttempts { get; set; }
        public DateTime? NextReviewDate { get; set; }
        public int SpacedRepInterval { get; set; }
        public int SpacedRepBox { get; set; }
        public DateTime? FirstStartedAt { get; set; }
        public DateTime? LastAttemptedAt { get; set; }
        public DateTime? MasteredAt { get; set; }

        // Navigation properties
        public StudentProfile? StudentProfile { get; set; }
        public StandardLesson? StandardLesson { get; set; }
    }
}
