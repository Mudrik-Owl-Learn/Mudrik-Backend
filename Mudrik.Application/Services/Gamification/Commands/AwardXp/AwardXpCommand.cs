using MediatR;
using Mudrik.Application.Services.Gamification.DTOs;

namespace Mudrik.Application.Services.Gamification.Commands.AwardXp
{
    public record AwardXpCommand(
        Guid StudentProfileId,
        string EventType,
        int BaseXp,
        int BonusXp,
        int? ReferenceId,
        string? ReferenceType
    ) : IRequest<XpAwardResultDto>;
}
