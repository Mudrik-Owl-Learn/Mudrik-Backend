using FluentValidation;

namespace Mudrik.Application.Services.Gamification.Queries.GetStreakStatus
{
    public class GetStreakStatusQueryValidator : AbstractValidator<GetStreakStatusQuery>
    {
        public GetStreakStatusQueryValidator()
        {
            RuleFor(x => x.StudentProfileId)
                .NotEmpty().WithMessage("StudentProfileId is required.");
        }
    }
}
