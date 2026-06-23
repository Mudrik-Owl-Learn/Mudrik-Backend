using System;

namespace Mudrik.Domain.Models
{
    public class GamificationStreak
    {
        public Guid Id { get; set; }
        public Guid StudentProfileId { get; set; }
        public int TotalPoints { get; set; }
        public int CurrentLevel { get; set; }
        public int CurrentStreak { get; set; }
        public int LongestStreak { get; set; }
        public DateOnly LastStreakDate { get; set; }
        public int FreezeTokensAvailable { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public StudentProfile StudentProfile { get; set; }
        public ICollection<XpTransaction> XpTransactions { get; set; } = new List<XpTransaction>();

        private GamificationStreak() { }

        public static GamificationStreak CreateForStudent(Guid studentProfileId, DateOnly today, int initialFreezeTokens = 1)
        {
            return new GamificationStreak
            {
                Id = Guid.NewGuid(),
                StudentProfileId = studentProfileId,
                TotalPoints = 0,
                CurrentLevel = 1,
                CurrentStreak = 0,
                LongestStreak = 0,
                LastStreakDate = today,
                FreezeTokensAvailable = initialFreezeTokens,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public void ApplyXp(int totalXpAwarded, int newLevel)
        {
            TotalPoints += totalXpAwarded;
            CurrentLevel = newLevel;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ContinueStreak(DateOnly today)
        {
            CurrentStreak += 1;
            if (CurrentStreak > LongestStreak) LongestStreak = CurrentStreak;
            LastStreakDate = today;
            UpdatedAt = DateTime.UtcNow;
        }

        public void StartOrRestartStreak(DateOnly today)
        {
            CurrentStreak = 1;
            if (LongestStreak < 1) LongestStreak = 1;
            LastStreakDate = today;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SaveStreakWithFreeze(DateOnly today)
        {
            CurrentStreak += 1;
            if (CurrentStreak > LongestStreak) LongestStreak = CurrentStreak;
            LastStreakDate = today;
            FreezeTokensAvailable -= 1;
            UpdatedAt = DateTime.UtcNow;
        }

        public void GrantFreezeToken()
        {
            FreezeTokensAvailable += 1;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
