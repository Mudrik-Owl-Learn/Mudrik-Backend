using FluentValidation;

namespace Mudrik.Application.Services.Lessons.Queries.GetAdaptedLesson
{
    public class GetAdaptedLessonQueryValidator : AbstractValidator<GetAdaptedLessonQuery>
    {
        public GetAdaptedLessonQueryValidator()
        {
            RuleFor(x => x.StandardLessonId)
                .NotEmpty().WithMessage("StandardLessonId is required.");

            RuleFor(x => x.StudentProfileId)
                .NotEmpty().WithMessage("StudentProfileId is required.");
        }
    }
}
