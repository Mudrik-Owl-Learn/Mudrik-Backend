using System.Collections.Generic;

namespace Mudrik.Domain.Models
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

        private Badge() { }

        public static Badge Create(
            string name,
            string description,
            string rarity,
            string imageUrl,
            string eligibilityCriteriaJson,
            bool isActive = true)
        {
            return new Badge
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Rarity = rarity,
                ImageUrl = imageUrl,
                EligibilityCriteriaJson = eligibilityCriteriaJson,
                IsActive = isActive
            };
        }
    }
   
}
