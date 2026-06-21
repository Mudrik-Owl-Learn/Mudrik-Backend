using System;

namespace Mudrik.Application.Services.Gamification.Helpers
{
    /// <summary>
    /// MVP leveling curve: Level = floor(sqrt(TotalPoints / 100)) + 1
    /// GamificationStreaks.CurrentLevel is a stored column (per ERD), not
    /// computed on read — this helper only determines what the new level
    /// SHOULD be after an XP award, so the handler can decide whether to
    /// persist a level-up and fire OnLevelUp.
    /// </summary>
    public static class LevelCalculator
    {
        private const int XpPerLevelUnit = 100;

        public static int GetLevelForTotalPoints(int totalPoints)
        {
            if (totalPoints < 0) totalPoints = 0;
            return (int)Math.Floor(Math.Sqrt(totalPoints / (double)XpPerLevelUnit)) + 1;
        }

        public static int GetPointsRequiredForLevel(int level)
        {
            if (level <= 1) return 0;
            var levelsAbove = level - 1;
            return levelsAbove * levelsAbove * XpPerLevelUnit;
        }

        public static int GetPointsIntoCurrentLevel(int totalPoints)
        {
            var level = GetLevelForTotalPoints(totalPoints);
            return totalPoints - GetPointsRequiredForLevel(level);
        }

        public static int GetPointsRequiredForNextLevel(int totalPoints)
        {
            var level = GetLevelForTotalPoints(totalPoints);
            return GetPointsRequiredForLevel(level + 1) - GetPointsRequiredForLevel(level);
        }
    }
}
