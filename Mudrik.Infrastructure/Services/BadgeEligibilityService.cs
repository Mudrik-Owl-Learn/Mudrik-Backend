using Mudrik.Application.Interfaces;
using Mudrik.Domain.Models;
using Mudrik.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Mudrik.Domain.Helpers;
using Mudrik.Domain.Enums;

namespace Mudrik.Infrastructure.Services
{
    public class BadgeEligibilityService(
        AppDbContext context,
        IBadgeRepository badgeRepository)
        : IBadgeEligibilityService
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<IReadOnlyList<Guid>> EvaluateAsync(
            Guid studentProfileId, CancellationToken cancellationToken)
        {
            // Only evaluate badges the student hasn't earned yet.
            var unearnedBadges = await badgeRepository
                .GetUnearnedActiveForStudentAsync(studentProfileId, cancellationToken);

            if (unearnedBadges.Count == 0) return Array.Empty<Guid>();

            // Load evaluation context once — no N+1.
            var streak = await context.GamificationStreaks
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StudentProfileId == studentProfileId, cancellationToken);

            var lessonsCompleted = await context.StudentLessonStates
                .AsNoTracking()
                .CountAsync(s => s.StudentProfileId == studentProfileId
                              && s.Status.Equals(LessonState.Mastered), cancellationToken);

            var highestQuizScore = await context.AgentGeneratedQuizzes
                .AsNoTracking()
                .Where(q => q.StudentProfileId == studentProfileId && q.IsPassed)
                .MaxAsync(q => (decimal?)q.ScorePercent, cancellationToken) ?? 0m;

            var eligible = new List<Guid>();

            foreach (var badge in unearnedBadges)
            {
                if (IsEligible(badge, streak, lessonsCompleted, highestQuizScore))
                    eligible.Add(badge.Id);
            }

            return eligible;
        }

        private static bool IsEligible(
            Badge badge,
            GamificationStreak? streak,
            int lessonsCompleted,
            decimal highestQuizScore)
        {
            BadgeEligibilityCriteria? criteria;

            try
            {
                criteria = JsonSerializer.Deserialize<BadgeEligibilityCriteria>(
                    badge.EligibilityCriteriaJson, JsonOptions);
            }
            catch
            {
                // Malformed JSON — skip badge silently, don't crash the engine.
                return false;
            }

            if (criteria is null) return false;

            return criteria.Type switch
            {
                BadgeEligibilityType.Streak =>
                    streak is not null && streak.CurrentStreak >= criteria.Threshold,

                BadgeEligibilityType.QuizScore =>
                    highestQuizScore >= criteria.Threshold,

                BadgeEligibilityType.LessonsCompleted =>
                    lessonsCompleted >= criteria.Threshold,

                // Unknown type — skip silently, engine stays running.
                _ => false
            };
        }
    }
}
