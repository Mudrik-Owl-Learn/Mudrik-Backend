using System;

namespace Mudrik.Domain.Models
{
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
        public Guid? ReferenceId { get; set; }
        public string ReferenceType { get; set; }
        public DateTime AwardedAt { get; set; }

        // Navigation properties
        public StudentProfile? StudentProfile { get; set; }
        public GamificationStreak? GamificationStreak { get; set; }

        private XpTransaction() { }

        public static XpTransaction Create(
            Guid studentProfileId,
            Guid gamificationStreakId,
            string eventType,
            int baseXp,
            int bonusXp,
            decimal streakMultiplier,
            Guid? referenceId,
            string? referenceType)
        {
            var total = (int)Math.Round((baseXp + bonusXp) * streakMultiplier, MidpointRounding.AwayFromZero);

            return new XpTransaction
            {
                Id = Guid.NewGuid(),
                StudentProfileId = studentProfileId,
                GamificationStreakId = gamificationStreakId,
                EventType = eventType,
                BaseXp = baseXp,
                BonusXp = bonusXp,
                StreakMultiplier = streakMultiplier,
                TotalXpAwarded = total,
                ReferenceId = referenceId,
                ReferenceType = referenceType,
                AwardedAt = DateTime.UtcNow
            };
        }
    }
}
