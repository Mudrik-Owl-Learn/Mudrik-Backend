using MediatR;
using Mudrik.Application.Services.LearnerProfile.DTOs;
using System;

namespace Mudrik.Application.Services.LearnerProfile.Commands.UpdateLearnerAIProfile
{
    public record UpdateLearnerAIProfileCommand(
        Guid StudentProfileId,
        int DyslexiaSeverity,
        int ADHDSeverity,
        int ReadingScore,
        int WritingScore,
        int ComprehensionScore,
        int AttentionSpanScore,
        int AttentionSpanMinutes,
        string PreferredFormat,
        string ChunkSizePref,
        string ConfidenceBias,
        bool AudioSupportRequired,
        int NumeracyLevel,
        int ReadingLevel,
        string DiagnosticResultJson
    ) : IRequest<LearnerAIProfileDto>;
}
