using FluentValidation;

namespace Mudrik.Application.Services.LearnerProfile.Queries.GetLearnerAIProfile
{
    public class GetLearnerAIProfileQueryValidator : AbstractValidator<GetLearnerAIProfileQuery>
    {
        public GetLearnerAIProfileQueryValidator()
        {
            RuleFor(x => x.StudentProfileId)
                .NotEmpty().WithMessage("StudentProfileId is required.");
        }
    }
}
