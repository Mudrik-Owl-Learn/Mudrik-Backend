using FluentValidation;

namespace Mudrik.Application.Services.Gamification.Commands.RecordDailyActivity
{
    public class RecordDailyActivityCommandValidator : AbstractValidator<RecordDailyActivityCommand>
    {
        public RecordDailyActivityCommandValidator()
        {
            RuleFor(x => x.StudentProfileId)
                .NotEmpty().WithMessage("StudentProfileId is required.");
        }
    }
}
