using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Gamification.DTOs;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Gamification.Commands.GrantFreezeToken
{
    public class GrantFreezeTokenCommandHandler(IGamificationStreakRepository streakRepository)
        : IRequestHandler<GrantFreezeTokenCommand, GamificationStreakDto>
    {
        public async Task<GamificationStreakDto> Handle(GrantFreezeTokenCommand request, CancellationToken cancellationToken)
        {
            var streak = await streakRepository.GetByStudentProfileIdAsync(request.StudentProfileId, cancellationToken)
                ?? throw new ValidationException("Gamification streak not found for this student.");

            streak.GrantFreezeToken();
            await streakRepository.UpdateAsync(streak, cancellationToken);

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            return new GamificationStreakDto(
                streak.Id,
                streak.StudentProfileId,
                streak.TotalPoints,
                streak.CurrentLevel,
                streak.CurrentStreak,
                streak.LongestStreak,
                streak.LastStreakDate,
                streak.FreezeTokensAvailable,
                IsAtRiskToday: streak.LastStreakDate.DayNumber < today.DayNumber,
                streak.UpdatedAt
            );
        }
    }
}
