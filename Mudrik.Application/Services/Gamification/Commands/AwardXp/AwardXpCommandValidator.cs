using FluentValidation;
using Mudrik.Domain.Models;

namespace Mudrik.Application.Services.Gamification.Commands.AwardXp
{
    public class AwardXpCommandValidator : AbstractValidator<AwardXpCommand>
    {
        private const int MaxBaseXp = 500;
        private const int MaxBonusXp = 500;

        private static readonly string[] ValidEventTypes =
        {
            XpEventType.MicroQuizCorrect,
            XpEventType.LessonChunkCompleted,
            XpEventType.LessonCompleted,
            XpEventType.DiagnosticQuizCompleted,
            XpEventType.DailyStreakBonus,
            XpEventType.BadgeUnlockedBonus,
            XpEventType.LevelUpBonus
        };

        public AwardXpCommandValidator()
        {
            RuleFor(x => x.StudentProfileId)
                .NotEmpty().WithMessage("StudentProfileId is required.");

            RuleFor(x => x.EventType)
                .NotEmpty().WithMessage("EventType is required.")
                .Must(et => ValidEventTypes.Contains(et))
                .WithMessage($"EventType must be one of: {string.Join(", ", ValidEventTypes)}");

            RuleFor(x => x.BaseXp)
                .GreaterThanOrEqualTo(0).WithMessage("BaseXp cannot be negative.")
                .LessThanOrEqualTo(MaxBaseXp).WithMessage($"BaseXp cannot exceed {MaxBaseXp}.");

            RuleFor(x => x.BonusXp)
                .GreaterThanOrEqualTo(0).WithMessage("BonusXp cannot be negative.")
                .LessThanOrEqualTo(MaxBonusXp).WithMessage($"BonusXp cannot exceed {MaxBonusXp}.");

            RuleFor(x => x)
                .Must(x => x.BaseXp > 0 || x.BonusXp > 0)
                .WithMessage("Either BaseXp or BonusXp must be greater than zero.");

            RuleFor(x => x.ReferenceType)
                .NotEmpty().WithMessage("ReferenceType is required when ReferenceId is provided.")
                .When(x => x.ReferenceId.HasValue);
        }
    }
}
