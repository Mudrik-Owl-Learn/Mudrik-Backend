using Mudrik.Domain.Entities;
using Mudrik.Domain.Models;
using System;

namespace Mudrik.Domain.Entities
{
    /// <summary>
    /// Maps 1:1 to the GamificationStreaks table in the ERD.
    /// This is the aggregate root for a student's gamification state —
    /// TotalPoints and CurrentLevel are persisted columns, not computed
    /// on the fly from XpTransactions. Every XpTransaction references
    /// the GamificationStreak row that was current at award time.
    /// </summary>
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


        private GamificationStreak() { } // EF Core

        public static GamificationStreak CreateForStudent(Guid studentProfileId, DateOnly today, int initialFreezeTokens = 1)
        {
            return new GamificationStreak
            {
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

        /// <summary>Applies XP earned in a single transaction to the running total and recalculates level.</summary>
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


