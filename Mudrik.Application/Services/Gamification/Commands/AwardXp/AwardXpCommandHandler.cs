
using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Badges.Commands.CheckAndAwardBadges;
using Mudrik.Application.Services.Gamification.DTOs;
using Mudrik.Application.Services.Gamification.Helpers;
using Mudrik.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Gamification.Commands.AwardXp
{
    public class AwardXpCommandHandler(
        IXpTransactionRepository xpRepository,
        IGamificationStreakRepository streakRepository,
        IGamificationNotifier notifier,
        IMediator mediator)
        : IRequestHandler<AwardXpCommand, XpAwardResultDto>
    {
        public async Task<XpAwardResultDto> Handle(AwardXpCommand request, CancellationToken cancellationToken)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            // Lazy creation — every student gets a GamificationStreaks row on first XP award.
            var streak = await streakRepository.GetByStudentProfileIdAsync(
                request.StudentProfileId, cancellationToken);

            if (streak is null)
            {
                streak = GamificationStreak.CreateForStudent(request.StudentProfileId, today);
                streak = await streakRepository.AddAsync(streak, cancellationToken);
            }

            var previousLevel = streak.CurrentLevel;
            var multiplier = StreakMultiplierCalculator.GetMultiplier(streak.CurrentStreak);

            var transaction = XpTransaction.Create(
                studentProfileId: request.StudentProfileId,
                gamificationStreakId: streak.Id,
                eventType: request.EventType,
                baseXp: request.BaseXp,
                bonusXp: request.BonusXp,
                streakMultiplier: multiplier,
                referenceId: request.ReferenceId,
                referenceType: request.ReferenceType);

            await xpRepository.AddAsync(transaction, cancellationToken);

            var newTotalPoints = streak.TotalPoints + transaction.TotalXpAwarded;
            var newLevel = LevelCalculator.GetLevelForTotalPoints(newTotalPoints);

            streak.ApplyXp(transaction.TotalXpAwarded, newLevel);
            await streakRepository.UpdateAsync(streak, cancellationToken);

            var leveledUp = newLevel > previousLevel;

            var result = new XpAwardResultDto(
                StudentProfileId: request.StudentProfileId,
                GamificationStreakId: streak.Id,
                EventType: transaction.EventType,
                BaseXp: transaction.BaseXp,
                BonusXp: transaction.BonusXp,
                StreakMultiplier: transaction.StreakMultiplier,
                TotalXpAwarded: transaction.TotalXpAwarded,
                TotalPoints: streak.TotalPoints,
                PreviousLevel: previousLevel,
                CurrentLevel: streak.CurrentLevel,
                LeveledUp: leveledUp
            );

            // Realtime push.
            await notifier.NotifyPointsEarnedAsync(request.StudentProfileId, result);
            if (leveledUp)
                await notifier.NotifyLevelUpAsync(request.StudentProfileId, streak.CurrentLevel);

            // Chain: XP awarded → check badge eligibility immediately.
            await mediator.Send(
                new CheckAndAwardBadgesCommand(request.StudentProfileId),
                cancellationToken);

            return result;
        }
    }
}
