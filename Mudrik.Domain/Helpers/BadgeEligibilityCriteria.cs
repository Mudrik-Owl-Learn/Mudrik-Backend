namespace Mudrik.Domain.Helpers
{
    public enum BadgeEligibilityType
    {
        Streak,
        QuizScore,
        LessonsCompleted
    }

    /// <summary>
    /// Strongly-typed representation of Badges.EligibilityCriteriaJson.
    /// Serialized as: {"type": "streak", "threshold": 3}
    /// </summary>
    public class BadgeEligibilityCriteria
    {
        public BadgeEligibilityType Type { get; set; }
        public int Threshold { get; set; }
    }
}
