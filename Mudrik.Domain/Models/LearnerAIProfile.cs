using System;

namespace Mudrik.Domain.Models
{
    public class LearnerAIProfile
    {
        /// <summary>
        /// The brain of the adaptive system — every downstream feature reads from this entity.
        /// Never overwrite a profile: always increment ProfileVersion and set LastUpdatedAt.
        /// </summary>
        public Guid Id { get; set; }
        public Guid StudentProfileId { get; private set; }

        // ---- Diagnostic scores ----
        public int DyslexiaSeverity { get; private set; }
        public int ADHDSeverity { get; private set; }
        public int ReadingScore { get; private set; }
        public int WritingScore { get; private set; }
        public int ComprehensionScore { get; private set; }
        public int AttentionSpanScore { get; private set; }
        public int AttentionSpanMinutes { get; private set; }

        // ---- Adaptation preferences ----
        public string PreferredFormat { get; private set; }
        public string ChunkSizePref { get; private set; }
        public string ConfidenceBias { get; private set; }
        public bool AudioSupportRequired { get; private set; }
        public int NumeracyLevel { get; private set; }
        public int ReadingLevel { get; private set; }

        // ---- Diagnostic raw output ----
        public string DiagnosticResultJson { get; private set; }

        // ---- Versioning ----
        public int ProfileVersion { get; private set; }
        public DateTime LastUpdatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        // Navigation properties
        public StudentProfile? StudentProfile { get; private set; }

        private LearnerAIProfile() { }

        public static LearnerAIProfile Create(
            Guid studentProfileId,
            int dyslexiaSeverity,
            int adhdSeverity,
            int readingScore,
            int writingScore,
            int comprehensionScore,
            int attentionSpanScore,
            int attentionSpanMinutes,
            string preferredFormat,
            string chunkSizePref,
            string confidenceBias,
            bool audioSupportRequired,
            int numeracyLevel,
            int readingLevel,
            string diagnosticResultJson)
        {
            return new LearnerAIProfile
            {
                Id = Guid.NewGuid(),
                StudentProfileId = studentProfileId,
                DyslexiaSeverity = dyslexiaSeverity,
                ADHDSeverity = adhdSeverity,
                ReadingScore = readingScore,
                WritingScore = writingScore,
                ComprehensionScore = comprehensionScore,
                AttentionSpanScore = attentionSpanScore,
                AttentionSpanMinutes = attentionSpanMinutes,
                PreferredFormat = preferredFormat,
                ChunkSizePref = chunkSizePref,
                ConfidenceBias = confidenceBias,
                AudioSupportRequired = audioSupportRequired,
                NumeracyLevel = numeracyLevel,
                ReadingLevel = readingLevel,
                DiagnosticResultJson = diagnosticResultJson,
                ProfileVersion = 1,
                LastUpdatedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Always increments ProfileVersion — never overwrites without versioning.
        /// Called after lesson completion or recalibration trigger.
        /// </summary>
        public void Update(
            int dyslexiaSeverity,
            int adhdSeverity,
            int readingScore,
            int writingScore,
            int comprehensionScore,
            int attentionSpanScore,
            int attentionSpanMinutes,
            string preferredFormat,
            string chunkSizePref,
            string confidenceBias,
            bool audioSupportRequired,
            int numeracyLevel,
            int readingLevel,
            string diagnosticResultJson)
        {
            DyslexiaSeverity = dyslexiaSeverity;
            ADHDSeverity = adhdSeverity;
            ReadingScore = readingScore;
            WritingScore = writingScore;
            ComprehensionScore = comprehensionScore;
            AttentionSpanScore = attentionSpanScore;
            AttentionSpanMinutes = attentionSpanMinutes;
            PreferredFormat = preferredFormat;
            ChunkSizePref = chunkSizePref;
            ConfidenceBias = confidenceBias;
            AudioSupportRequired = audioSupportRequired;
            NumeracyLevel = numeracyLevel;
            ReadingLevel = readingLevel;
            DiagnosticResultJson = diagnosticResultJson;
            ProfileVersion += 1;
            LastUpdatedAt = DateTime.UtcNow;
        }
    }
}
