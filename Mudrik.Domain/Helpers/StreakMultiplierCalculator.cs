namespace Mudrik.Application.Services.Gamification.Helpers
{
    /// <summary>
    /// Computes XpTransactions.StreakMultiplier from the student's current
    /// streak length. Longer streaks award a small bonus multiplier to
    /// reward daily consistency (the "Streak Fire" mechanic).
    /// </summary>
    public static class StreakMultiplierCalculator
    {
        public static decimal GetMultiplier(int currentStreakDays)
        {
            return currentStreakDays switch
            {
                <= 0 => 1.0m,
                >= 1 and <= 2 => 1.0m,
                >= 3 and <= 6 => 1.1m,
                >= 7 and <= 13 => 1.25m,
                >= 14 and <= 29 => 1.5m,
                >= 30 => 2.0m
            };
        }
    }
}
