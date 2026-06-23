using MediatR;
using Mudrik.Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Badges.Commands.MarkBadgeDisplayed
{
    public class MarkBadgeDisplayedCommandHandler(IBadgeRepository badgeRepository)
        : IRequestHandler<MarkBadgeDisplayedCommand>
    {
        public async Task Handle(MarkBadgeDisplayedCommand request, CancellationToken cancellationToken)
        {
            var studentBadge = await badgeRepository.GetStudentBadgeAsync(
                request.StudentProfileId, request.BadgeId, cancellationToken);

            if (studentBadge is null)
                throw new ValidationException("Badge not found for this student.");

            await badgeRepository.MarkDisplayedAsync(
                request.StudentProfileId, request.BadgeId, cancellationToken);
        }
    }
}
