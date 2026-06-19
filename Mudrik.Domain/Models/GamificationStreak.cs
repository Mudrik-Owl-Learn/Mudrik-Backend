using System;
using System.Collections.Generic;

namespace Mudrik.Domain.Entities
{
    public class GamificationStreak
    {
        public int Id { get; set; }
        public int StudentProfileId { get; set; }
        public int TotalPoints { get; set; }
        public int CurrentLevel { get; set; }
        public int CurrentStreak { get; set; }
        public int LongestStreak { get; set; }
        public DateTime? LastStreakDate { get; set; }
        public int FreezeTokensAvailable { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public StudentProfile StudentProfile { get; set; }
        public ICollection<XpTransaction> XpTransactions { get; set; } = new List<XpTransaction>();
    }
}
