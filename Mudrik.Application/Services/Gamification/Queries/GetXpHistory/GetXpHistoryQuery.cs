using MediatR;
using Mudrik.Application.Services.Gamification.DTOs;

namespace Mudrik.Application.Services.Gamification.Queries.GetXpHistory
{
    public record GetXpHistoryQuery(
        Guid StudentProfileId,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<XpHistoryDto>;
}
