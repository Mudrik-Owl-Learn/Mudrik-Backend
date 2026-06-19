using System;

namespace Mudrik.Domain.Entities
{
    public class XpTransaction
    {
        public int Id { get; set; }
        public int StudentProfileId { get; set; }
        public int GamificationStreakId { get; set; }
        public string EventType { get; set; }
        public int BaseXp { get; set; }
        public int BonusXp { get; set; }
        public decimal StreakMultiplier { get; set; }
        public int TotalXpAwarded { get; set; }
        public int? ReferenceId { get; set; }
        public string ReferenceType { get; set; }
        public DateTime AwardedAt { get; set; }

        // Navigation properties
        public StudentProfile? StudentProfile { get; set; }
        public GamificationStreak? GamificationStreak { get; set; }
    }
}
