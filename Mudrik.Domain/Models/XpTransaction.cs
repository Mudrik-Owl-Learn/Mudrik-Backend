using Mudrik.Domain.Entities;
using System;

namespace Mudrik.Domain.Models
{
    /// <summary>
    /// Maps 1:1 to the XpTransactions table in the ERD.
    /// Append-only audit log: every row records exactly what was awarded,
    /// why (EventType + polymorphic Reference), and which GamificationStreak
    /// snapshot it was applied against.
    /// </summary>
    public class XpTransaction
    {
        public Guid Id { get; set; }
        public Guid StudentProfileId { get; set; }
        public Guid GamificationStreakId { get; set; }
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

        private XpTransaction() { } // EF Core

       
        public static XpTransaction Create(
            Guid studentProfileId,
            Guid gamificationStreakId,
            string eventType,
            int baseXp,
            int bonusXp,
            decimal streakMultiplier,
            int? referenceId,
            string? referenceType)
        {
            var totalXpAwarded = (int)Math.Round((baseXp + bonusXp) * streakMultiplier, MidpointRounding.AwayFromZero);

            return new XpTransaction
            {
                StudentProfileId = studentProfileId,
                GamificationStreakId = gamificationStreakId,
                EventType = eventType,
                BaseXp = baseXp,
                BonusXp = bonusXp,
                StreakMultiplier = streakMultiplier,
                TotalXpAwarded = totalXpAwarded,
                ReferenceId = referenceId,
                ReferenceType = referenceType,
                AwardedAt = DateTime.UtcNow
            };
        }
    }
}


