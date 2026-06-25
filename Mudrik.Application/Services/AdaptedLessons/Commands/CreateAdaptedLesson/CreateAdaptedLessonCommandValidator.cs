using FluentValidation;

namespace Mudrik.Application.Services.Lessons.Commands.CreateAdaptedLesson
{
    public class CreateAdaptedLessonCommandValidator : AbstractValidator<CreateAdaptedLessonCommand>
    {
        public CreateAdaptedLessonCommandValidator()
        {
            RuleFor(x => x.StudentProfileId)
                .NotEmpty().WithMessage("StudentProfileId is required.");

            RuleFor(x => x.StandardLessonId)
                .NotEmpty().WithMessage("StandardLessonId is required.");
        }
    }
}
