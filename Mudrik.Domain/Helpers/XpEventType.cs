namespace Mudrik.Domain.Models
{
    /// <summary>
    /// Canonical XpTransactions.EventType values. Stored as NVARCHAR in the
    /// ERD for flexibility, but the application should only ever write one
    /// of these — never a free-form string.
    /// </summary>
    public static class XpEventType
    {
        public const string MicroQuizCorrect = "MICRO_QUIZ_CORRECT";
        public const string LessonChunkCompleted = "LESSON_CHUNK_COMPLETED";
        public const string LessonCompleted = "LESSON_COMPLETED";
        public const string DiagnosticQuizCompleted = "DIAGNOSTIC_QUIZ_COMPLETED";
        public const string DailyStreakBonus = "DAILY_STREAK_BONUS";
        public const string BadgeUnlockedBonus = "BADGE_UNLOCKED_BONUS";
        public const string LevelUpBonus = "LEVEL_UP_BONUS";
    }

    /// <summary>
    /// Canonical XpTransactions.ReferenceType values, pairing with ReferenceId
    /// to point at the row that triggered the award (polymorphic reference).
    /// </summary>
    public static class XpReferenceType
    {
        public const string AgentGeneratedQuiz = "AGENT_GENERATED_QUIZ";
        public const string LessonMicroChunk = "LESSON_MICRO_CHUNK";
        public const string StandardLesson = "STANDARD_LESSON";
        public const string Badge = "BADGE";
        public const string None = "NONE";
    }
}
