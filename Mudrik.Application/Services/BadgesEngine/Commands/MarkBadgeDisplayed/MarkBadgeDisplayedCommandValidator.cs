using FluentValidation;

namespace Mudrik.Application.Services.Badges.Commands.MarkBadgeDisplayed
{
    public class MarkBadgeDisplayedCommandValidator : AbstractValidator<MarkBadgeDisplayedCommand>
    {
        public MarkBadgeDisplayedCommandValidator()
        {
            RuleFor(x => x.StudentProfileId)
                .NotEmpty().WithMessage("StudentProfileId is required.");

            RuleFor(x => x.BadgeId)
                .NotEmpty().WithMessage("BadgeId is required.");
        }
    }
}
