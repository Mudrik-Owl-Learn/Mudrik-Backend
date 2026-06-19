using System;

namespace Mudrik.Domain.Entities
{
    public class StudentBadge
    {
        public int Id { get; set; }
        public int StudentProfileId { get; set; }
        public int BadgeId { get; set; }
        public bool HasBeenDisplayed { get; set; }
        public DateTime EarnedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public StudentProfile? StudentProfile { get; set; }
        public Badge? Badge { get; set; }
    }
}
