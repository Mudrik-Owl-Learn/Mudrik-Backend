using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudrik.Domain.Models;

namespace Mudrik.Domain.Configurations
{
    public class StudentProfileConfiguration : IEntityTypeConfiguration<StudentProfile>
    {
        public void Configure(EntityTypeBuilder<StudentProfile> builder)
        {
            builder.ToTable("StudentProfiles", t =>
            {
                // CHECK constraint on StudentProfiles: GradeLevel BETWEEN 1 AND 4, Age BETWEEN 4 AND 12
                t.HasCheckConstraint("CK_StudentProfiles_GradeLevel", "[GradeLevel] BETWEEN 1 AND 4");
                t.HasCheckConstraint("CK_StudentProfiles_Age", "[Age] BETWEEN 4 AND 12");
            });

            builder.HasKey(s => s.Id);

            builder.Property(a => a.Id)
    .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(s => s.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Gender)
                .HasMaxLength(20);

            builder.Property(s => s.AvatarId)
                .HasMaxLength(100);

            builder.Property(s => s.HasDyslexia)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(s => s.HasADHD)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(s => s.FontPreference)
                .HasMaxLength(50);

            builder.Property(s => s.ColorOverlay)
                .HasMaxLength(50);

            builder.Property(s => s.AudioEnabled)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(s => s.InterestsJson)
                .HasColumnType("nvarchar(max)")
                .HasDefaultValue("[]");

            builder.Property(s => s.LearningStylePref)
                .HasMaxLength(50);

            builder.Property(s => s.PersonalityTag)
                .HasMaxLength(50);

            builder.Property(s => s.OnboardingComplete)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(s => s.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            // Relationships

            // StudentProfiles.ParentProfileId -> ParentProfiles.Id : RESTRICT
            builder.HasOne(s => s.ParentProfile)
                .WithMany(p => p.StudentProfiles)
                .HasForeignKey(s => s.ParentProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.LearnerAIProfile)
                .WithOne(l => l.StudentProfile)
                .HasForeignKey<LearnerAIProfile>(l => l.StudentProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.GamificationStreak)
                .WithOne(g => g.StudentProfile)
                .HasForeignKey<GamificationStreak>(g => g.StudentProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.AdaptedLessons)
                .WithOne(a => a.StudentProfile)
                .HasForeignKey(a => a.StudentProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.LessonMicroChunks)
                .WithOne(m => m.StudentProfile)
                .HasForeignKey(m => m.StudentProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.StudentLessonStates)
                .WithOne(sl => sl.StudentProfile)
                .HasForeignKey(sl => sl.StudentProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.AgentGeneratedQuizzes)
                .WithOne(q => q.StudentProfile)
                .HasForeignKey(q => q.StudentProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.StudentQuizAnswers)
                .WithOne(a => a.StudentProfile)
                .HasForeignKey(a => a.StudentProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.XpTransactions)
                .WithOne(x => x.StudentProfile)
                .HasForeignKey(x => x.StudentProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.StudentBadges)
                .WithOne(b => b.StudentProfile)
                .HasForeignKey(b => b.StudentProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
