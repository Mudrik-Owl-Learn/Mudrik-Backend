using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Gamification.DTOs;
using Mudrik.Domain.Entities;
using Mudrik.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Gamification.Commands.RecordDailyActivity
{
    public class RecordDailyActivityCommandHandler(
        IGamificationStreakRepository streakRepository,
        IGamificationNotifier notifier)
        : IRequestHandler<RecordDailyActivityCommand, StreakUpdateResultDto>
    {
        public async Task<StreakUpdateResultDto> Handle(RecordDailyActivityCommand request, CancellationToken cancellationToken)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var streak = await streakRepository.GetByStudentProfileIdAsync(request.StudentProfileId, cancellationToken);

            StreakUpdateOutcome outcome;
            bool isNewLongest;

            if (streak is null)
            {
                streak = GamificationStreak.CreateForStudent(request.StudentProfileId, today);
                streak.StartOrRestartStreak(today);
                await streakRepository.AddAsync(streak, cancellationToken);
                outcome = StreakUpdateOutcome.Started;
                isNewLongest = true;
            }
            else
            {
                var daysSinceLastActivity = today.DayNumber - streak.LastStreakDate.DayNumber;

                if (daysSinceLastActivity == 0)
                {
                    outcome = StreakUpdateOutcome.AlreadyRecordedToday;
                    isNewLongest = false;
                }
                else if (daysSinceLastActivity == 1)
                {
                    var previousLongest = streak.LongestStreak;
                    streak.ContinueStreak(today);
                    await streakRepository.UpdateAsync(streak, cancellationToken);
                    outcome = StreakUpdateOutcome.Continued;
                    isNewLongest = streak.LongestStreak > previousLongest;
                }
                else if (daysSinceLastActivity == 2 && streak.FreezeTokensAvailable > 0)
                {
                    var previousLongest = streak.LongestStreak;
                    streak.SaveStreakWithFreeze(today);
                    await streakRepository.UpdateAsync(streak, cancellationToken);
                    outcome = StreakUpdateOutcome.SavedByFreeze;
                    isNewLongest = streak.LongestStreak > previousLongest;
                }
                else
                {
                    streak.StartOrRestartStreak(today);
                    await streakRepository.UpdateAsync(streak, cancellationToken);
                    outcome = StreakUpdateOutcome.BrokenAndRestarted;
                    isNewLongest = false;
                }
            }

            var result = new StreakUpdateResultDto(
                StudentProfileId: request.StudentProfileId,
                Outcome: outcome,
                CurrentStreak: streak.CurrentStreak,
                LongestStreak: streak.LongestStreak,
                FreezeTokensAvailable: streak.FreezeTokensAvailable,
                IsNewLongestStreak: isNewLongest
            );

            if (outcome != StreakUpdateOutcome.AlreadyRecordedToday)
            {
                await notifier.NotifyStreakUpdatedAsync(request.StudentProfileId, result);
            }

            return result;
        }
    }
}
