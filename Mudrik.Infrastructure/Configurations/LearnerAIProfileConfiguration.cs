using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudrik.Domain.Entities;

namespace Mudrik.Domain.Configurations
{
    public class LearnerAIProfileConfiguration : IEntityTypeConfiguration<LearnerAIProfile>
    {
        public void Configure(EntityTypeBuilder<LearnerAIProfile> builder)
        {
            builder.ToTable("LearnerAIProfiles", t =>
            {
                // CHECK constraints: severities 0-5, scores 0-100
                t.HasCheckConstraint("CK_LearnerAIProfiles_DyslexiaSeverity", "[DyslexiaSeverity] BETWEEN 0 AND 5");
                t.HasCheckConstraint("CK_LearnerAIProfiles_ADHDSeverity", "[ADHDSeverity] BETWEEN 0 AND 5");
                t.HasCheckConstraint("CK_LearnerAIProfiles_ReadingScore", "[ReadingScore] BETWEEN 0 AND 100");
                t.HasCheckConstraint("CK_LearnerAIProfiles_WritingScore", "[WritingScore] BETWEEN 0 AND 100");
                t.HasCheckConstraint("CK_LearnerAIProfiles_ComprehensionScore", "[ComprehensionScore] BETWEEN 0 AND 100");
                t.HasCheckConstraint("CK_LearnerAIProfiles_AttentionSpanScore", "[AttentionSpanScore] BETWEEN 0 AND 100");
            });

            builder.HasKey(l => l.Id);

            builder.Property(l => l.PreferredFormat)
                .HasMaxLength(50);

            builder.Property(l => l.ChunkSizePref)
                .HasMaxLength(50);

            builder.Property(l => l.ConfidenceBias)
                .HasMaxLength(50);

            builder.Property(l => l.AudioSupportRequired)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(l => l.DiagnosticResultJson)
                .HasColumnType("nvarchar(max)")
                .HasDefaultValue("[]");

            builder.Property(l => l.ProfileVersion)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(l => l.LastUpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(l => l.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            // 1:1 with Students
            builder.HasIndex(l => l.StudentProfileId)
                .IsUnique();

            // Relationships
            // LearnerAIProfiles.StudentProfileId -> StudentProfiles.Id : CASCADE
            builder.HasOne(l => l.StudentProfile)
                .WithOne(s => s.LearnerAIProfile)
                .HasForeignKey<LearnerAIProfile>(l => l.StudentProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
