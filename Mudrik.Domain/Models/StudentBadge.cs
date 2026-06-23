using Mudrik.Domain.Models;
using System;

namespace Mudrik.Domain.Models
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

        private StudentBadge() { }

        public static StudentBadge Create(Guid studentProfileId, Guid badgeId)
        {
            return new StudentBadge
            {
                Id = Guid.NewGuid(),
                StudentProfileId = studentProfileId,
                BadgeId = badgeId,
                HasBeenDisplayed = false,
                EarnedAt = DateTime.UtcNow
            };
        }

        public void MarkAsDisplayed()
        {
            HasBeenDisplayed = true;
        }
    }

}
