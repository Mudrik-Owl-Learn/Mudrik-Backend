using FluentValidation;

namespace Mudrik.Application.Services.Gamification.Commands.GrantFreezeToken
{
    public class GrantFreezeTokenCommandValidator : AbstractValidator<GrantFreezeTokenCommand>
    {
        public GrantFreezeTokenCommandValidator()
        {
            RuleFor(x => x.StudentProfileId)
                .NotEmpty().WithMessage("StudentProfileId is required.");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Reason is required.")
                .MaximumLength(200);
        }
    }
}
