using System;

namespace Mudrik.Domain.Entities
{
    public class LearnerAIProfile
    {
        public Guid Id { get; set; }
        public Guid StudentProfileId { get; set; }
        public int DyslexiaSeverity { get; set; }
        public int ADHDSeverity { get; set; }
        public int ReadingScore { get; set; }
        public int WritingScore { get; set; }
        public int ComprehensionScore { get; set; }
        public int AttentionSpanScore { get; set; }
        public int AttentionSpanMinutes { get; set; }
        public string PreferredFormat { get; set; }
        public string ChunkSizePref { get; set; }
        public string ConfidenceBias { get; set; }
        public bool AudioSupportRequired { get; set; }
        public int NumeracyLevel { get; set; }
        public int ReadingLevel { get; set; }
        public string DiagnosticResultJson { get; set; }
        public int ProfileVersion { get; set; }
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public StudentProfile? StudentProfile { get; set; }
    }
}
