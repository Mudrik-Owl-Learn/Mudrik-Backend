using System;
using System.Collections.Generic;

namespace Mudrik.Domain.Models
{
    public class ParentProfile
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public bool ConsentGiven { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ApplicationUser? User { get; set; }
        public ICollection<StudentProfile> StudentProfiles { get; set; } = new List<StudentProfile>();
    }
}
