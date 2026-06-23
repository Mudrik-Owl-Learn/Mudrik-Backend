using FluentValidation;

namespace Mudrik.Application.Services.Badges.Queries.GetStudentBadges
{
    public class GetStudentBadgesQueryValidator : AbstractValidator<GetStudentBadgesQuery>
    {
        public GetStudentBadgesQueryValidator()
        {
            RuleFor(x => x.StudentProfileId)
                .NotEmpty().WithMessage("StudentProfileId is required.");
        }
    }
}
