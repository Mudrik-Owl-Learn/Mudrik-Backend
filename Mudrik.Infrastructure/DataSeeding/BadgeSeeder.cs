using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Mudrik.Infrastructure.DataSeeding
{
    /// <summary>
    /// Initial badge seed — one badge per eligibility type, low thresholds for testing.
    /// Real threshold values to be updated in a follow-up migration before production.
    /// Apply by calling: dotnet ef migrations add SeedInitialBadges
    /// then referencing this class in the Up() method.
    /// </summary>
    public static class BadgeSeeder
    {
        // Stable GUIDs so re-running the seed is idempotent.
        public static readonly Guid StreakBadgeId       = new("a1b2c3d4-0001-0000-0000-000000000001");
        public static readonly Guid QuizScoreBadgeId    = new("a1b2c3d4-0002-0000-0000-000000000002");
        public static readonly Guid LessonsCompletedId  = new("a1b2c3d4-0003-0000-0000-000000000003");

        public static void Seed(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Badges",
                columns: new[]
                {
                    "Id", "Name", "Description", "Rarity",
                    "ImageUrl", "EligibilityCriteriaJson", "IsActive"
                },
                values: new object[,]
                {
                    {
                        StreakBadgeId,
                        "أسبوع المثابرة",
                        "تعلّمت 3 أيام متتالية — استمر!",
                        "Common",
                        "/badges/streak_3.png",
                        "{\"type\":\"streak\",\"threshold\":3}",
                        true
                    },
                    {
                        QuizScoreBadgeId,
                        "العبقري الصغير",
                        "حصلت على 100% في اختبار واحد على الأقل.",
                        "Rare",
                        "/badges/quiz_100.png",
                        "{\"type\":\"quizScore\",\"threshold\":100}",
                        true
                    },
                    {
                        LessonsCompletedId,
                        "أول خطوة",
                        "أكملت أول درس لك — الرحلة بدأت!",
                        "Common",
                        "/badges/lesson_1.png",
                        "{\"type\":\"lessonsCompleted\",\"threshold\":1}",
                        true
                    }
                }
            );
        }

        public static void Unseed(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Badges",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    StreakBadgeId,
                    QuizScoreBadgeId,
                    LessonsCompletedId
                }
            );
        }
    }
}
