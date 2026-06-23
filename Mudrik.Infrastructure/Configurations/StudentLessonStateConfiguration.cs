using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudrik.Domain.Models;

namespace Mudrik.Domain.Configurations
{
    public class StudentLessonStateConfiguration : IEntityTypeConfiguration<StudentLessonState>
    {
        public void Configure(EntityTypeBuilder<StudentLessonState> builder)
        {
            builder.ToTable("StudentLessonStates");

            builder.HasKey(s => s.Id);

            builder.Property(a => a.Id)
    .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(s => s.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.AverageQuizScore)
                .IsRequired()
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(0m);

            builder.Property(s => s.TotalAttempts)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(s => s.NextReviewDate)
                .HasColumnType("date");

            builder.Property(s => s.SpacedRepInterval)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(s => s.SpacedRepBox)
                .IsRequired()
                .HasDefaultValue(1);

            // One state machine row per student per lesson
            builder.HasIndex(s => new { s.StudentProfileId, s.StandardLessonId })
                .IsUnique();

            // Relationships
            // StudentLessonStates.StudentProfileId -> StudentProfiles.Id : CASCADE
            builder.HasOne(s => s.StudentProfile)
                .WithMany(sp => sp.StudentLessonStates)
                .HasForeignKey(s => s.StudentProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // StudentLessonStates.StandardLessonId -> StandardLessons.Id : RESTRICT
            builder.HasOne(s => s.StandardLesson)
                .WithMany(l => l.StudentLessonStates)
                .HasForeignKey(s => s.StandardLessonId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
