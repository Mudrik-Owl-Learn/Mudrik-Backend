using System.Collections.Generic;

namespace Mudrik.Application.Services.Gamification.DTOs
{
    public record XpHistoryDto(
        IReadOnlyList<XpTransactionDto> Transactions,
        int TotalCount,
        int PageNumber,
        int PageSize,
        int TotalPoints,
        int CurrentLevel
    );
}
