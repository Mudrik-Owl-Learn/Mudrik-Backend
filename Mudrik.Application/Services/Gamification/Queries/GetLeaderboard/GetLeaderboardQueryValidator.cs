using FluentValidation;

namespace Mudrik.Application.Services.Gamification.Queries.GetLeaderboard
{
    public class GetLeaderboardQueryValidator : AbstractValidator<GetLeaderboardQuery>
    {
        public GetLeaderboardQueryValidator()
        {
            RuleFor(x => x.Top)
                .InclusiveBetween(1, 50).WithMessage("Top must be between 1 and 50.");

            RuleFor(x => x.GradeLevel)
                .NotNull().WithMessage("GradeLevel is required when Scope is ByGradeLevel.")
                .When(x => x.Scope == LeaderboardScope.ByGradeLevel);

            RuleFor(x => x.GradeLevel)
                .InclusiveBetween(1, 4).WithMessage("GradeLevel must be between 1 and 4.")
                .When(x => x.GradeLevel.HasValue);
        }
    }
}
