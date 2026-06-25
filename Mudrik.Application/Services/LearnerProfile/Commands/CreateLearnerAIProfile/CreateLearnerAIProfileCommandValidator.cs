using FluentValidation;

namespace Mudrik.Application.Services.LearnerProfile.Commands.CreateLearnerAIProfile
{
    public class CreateLearnerAIProfileCommandValidator : AbstractValidator<CreateLearnerAIProfileCommand>
    {
        private static readonly string[] ValidFormats =
            { "VisualExplainer", "StoryBased", "AudioNarration", "Standard" };

        private static readonly string[] ValidChunkSizes =
            { "Short", "Medium", "Long" };

        public CreateLearnerAIProfileCommandValidator()
        {
            RuleFor(x => x.StudentProfileId)
                .NotEmpty().WithMessage("StudentProfileId is required.");

            RuleFor(x => x.DyslexiaSeverity)
                .InclusiveBetween(0, 100).WithMessage("DyslexiaSeverity must be between 0 and 100.");

            RuleFor(x => x.ADHDSeverity)
                .InclusiveBetween(0, 100).WithMessage("ADHDSeverity must be between 0 and 100.");

            RuleFor(x => x.ReadingScore)
                .InclusiveBetween(0, 100).WithMessage("ReadingScore must be between 0 and 100.");

            RuleFor(x => x.WritingScore)
                .InclusiveBetween(0, 100).WithMessage("WritingScore must be between 0 and 100.");

            RuleFor(x => x.ComprehensionScore)
                .InclusiveBetween(0, 100).WithMessage("ComprehensionScore must be between 0 and 100.");

            RuleFor(x => x.AttentionSpanScore)
                .InclusiveBetween(0, 100).WithMessage("AttentionSpanScore must be between 0 and 100.");

            RuleFor(x => x.AttentionSpanMinutes)
                .GreaterThan(0).WithMessage("AttentionSpanMinutes must be greater than 0.")
                .LessThanOrEqualTo(60).WithMessage("AttentionSpanMinutes must not exceed 60.");

            RuleFor(x => x.PreferredFormat)
                .NotEmpty()
                .Must(f => ValidFormats.Contains(f))
                .WithMessage($"PreferredFormat must be one of: {string.Join(", ", ValidFormats)}");

            RuleFor(x => x.ChunkSizePref)
                .NotEmpty()
                .Must(c => ValidChunkSizes.Contains(c))
                .WithMessage($"ChunkSizePref must be one of: {string.Join(", ", ValidChunkSizes)}");

            RuleFor(x => x.DiagnosticResultJson)
                .NotEmpty().WithMessage("DiagnosticResultJson is required.");
        }
    }
}
