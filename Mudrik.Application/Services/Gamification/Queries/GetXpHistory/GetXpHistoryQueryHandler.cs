using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Gamification.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Gamification.Queries.GetXpHistory
{
    public class GetXpHistoryQueryHandler(
        IXpTransactionRepository xpRepository,
        IGamificationStreakRepository streakRepository)
        : IRequestHandler<GetXpHistoryQuery, XpHistoryDto>
    {
        public async Task<XpHistoryDto> Handle(GetXpHistoryQuery request, CancellationToken cancellationToken)
        {
            var (items, totalCount) = await xpRepository.GetPagedForStudentAsync(
                request.StudentProfileId, request.PageNumber, request.PageSize, cancellationToken);

            var streak = await streakRepository.GetByStudentProfileIdAsync(request.StudentProfileId, cancellationToken);

            var dtoItems = items.Select(t => new XpTransactionDto(
                t.Id,
                t.StudentProfileId,
                t.GamificationStreakId,
                t.EventType,
                t.BaseXp,
                t.BonusXp,
                t.StreakMultiplier,
                t.TotalXpAwarded,
                t.ReferenceId,
                t.ReferenceType,
                t.AwardedAt
            )).ToList();

            return new XpHistoryDto(
                Transactions: dtoItems,
                TotalCount: totalCount,
                PageNumber: request.PageNumber,
                PageSize: request.PageSize,
                TotalPoints: streak?.TotalPoints ?? 0,
                CurrentLevel: streak?.CurrentLevel ?? 1
            );
        }
    }
}
