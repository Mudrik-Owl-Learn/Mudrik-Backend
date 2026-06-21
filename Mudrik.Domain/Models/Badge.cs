using System.Collections.Generic;

namespace Mudrik.Domain.Entities
{
    public class Badge
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rarity { get; set; }
        public string ImageUrl { get; set; }
        public string EligibilityCriteriaJson { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public ICollection<StudentBadge> StudentBadges { get; set; } = new List<StudentBadge>();
    }
}
