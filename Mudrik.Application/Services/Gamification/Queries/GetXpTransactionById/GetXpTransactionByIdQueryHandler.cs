using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Gamification.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Gamification.Queries.GetXpTransactionById
{
    public class GetXpTransactionByIdQueryHandler(IXpTransactionRepository xpRepository)
        : IRequestHandler<GetXpTransactionByIdQuery, XpTransactionDto?>
    {
        public async Task<XpTransactionDto?> Handle(GetXpTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var t = await xpRepository.GetByIdAsync(request.Id, cancellationToken);
            if (t is null) return null;

            return new XpTransactionDto(
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
            );
        }
    }
}
