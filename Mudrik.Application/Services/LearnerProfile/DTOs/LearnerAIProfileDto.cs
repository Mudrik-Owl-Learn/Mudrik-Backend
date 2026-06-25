using System;

namespace Mudrik.Application.Services.LearnerProfile.DTOs
{
    public record LearnerAIProfileDto(
        Guid Id,
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
        string DiagnosticResultJson,
        int ProfileVersion,
        DateTime LastUpdatedAt,
        DateTime CreatedAt
    );
}
