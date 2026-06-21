using FluentValidation;

namespace Mudrik.Application.Services.Gamification.Queries.GetXpHistory
{
    public class GetXpHistoryQueryValidator : AbstractValidator<GetXpHistoryQuery>
    {
        public GetXpHistoryQueryValidator()
        {
            RuleFor(x => x.StudentProfileId)
                .NotEmpty().WithMessage("StudentProfileId is required.");

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");
        }
    }
}
