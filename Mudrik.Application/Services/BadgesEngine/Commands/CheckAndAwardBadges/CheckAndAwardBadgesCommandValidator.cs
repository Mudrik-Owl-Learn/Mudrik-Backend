using FluentValidation;

namespace Mudrik.Application.Services.Badges.Commands.CheckAndAwardBadges
{
    public class CheckAndAwardBadgesCommandValidator : AbstractValidator<CheckAndAwardBadgesCommand>
    {
        public CheckAndAwardBadgesCommandValidator()
        {
            RuleFor(x => x.StudentProfileId)
                .NotEmpty().WithMessage("StudentProfileId is required.");
        }
    }
}
