using FluentValidation;

namespace Mudrik.Application.Services.LearnerProfile.Commands.UpdateLearnerAIProfile
{
    public class UpdateLearnerAIProfileCommandValidator : AbstractValidator<UpdateLearnerAIProfileCommand>
    {
        private static readonly string[] ValidFormats =
            { "VisualExplainer", "StoryBased", "AudioNarration", "Standard" };

        private static readonly string[] ValidChunkSizes =
            { "Short", "Medium", "Long" };

        public UpdateLearnerAIProfileCommandValidator()
        {
            RuleFor(x => x.StudentProfileId)
                .NotEmpty().WithMessage("StudentProfileId is required.");

            RuleFor(x => x.DyslexiaSeverity)
                .InclusiveBetween(0, 100);

            RuleFor(x => x.ADHDSeverity)
                .InclusiveBetween(0, 100);

            RuleFor(x => x.ReadingScore)
                .InclusiveBetween(0, 100);

            RuleFor(x => x.WritingScore)
                .InclusiveBetween(0, 100);

            RuleFor(x => x.ComprehensionScore)
                .InclusiveBetween(0, 100);

            RuleFor(x => x.AttentionSpanScore)
                .InclusiveBetween(0, 100);

            RuleFor(x => x.AttentionSpanMinutes)
                .GreaterThan(0).LessThanOrEqualTo(60);

            RuleFor(x => x.PreferredFormat)
                .NotEmpty()
                .Must(f => ValidFormats.Contains(f))
                .WithMessage($"PreferredFormat must be one of: {string.Join(", ", ValidFormats)}");

            RuleFor(x => x.ChunkSizePref)
                .NotEmpty()
                .Must(c => ValidChunkSizes.Contains(c))
                .WithMessage($"ChunkSizePref must be one of: {string.Join(", ", ValidChunkSizes)}");

            RuleFor(x => x.DiagnosticResultJson)
                .NotEmpty();
        }
    }
}
