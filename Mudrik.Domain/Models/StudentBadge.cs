using System;

namespace Mudrik.Domain.Entities
{
    public class StudentBadge
    {
        public Guid Id { get; set; }
        public Guid StudentProfileId { get; set; }
        public Guid BadgeId { get; set; }
        public bool HasBeenDisplayed { get; set; }
        public DateTime EarnedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public StudentProfile? StudentProfile { get; set; }
        public Badge? Badge { get; set; }
    }
}
