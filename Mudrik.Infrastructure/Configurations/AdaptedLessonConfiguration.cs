using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudrik.Domain.Entities;

namespace Mudrik.Domain.Configurations
{
    public class AdaptedLessonConfiguration : IEntityTypeConfiguration<AdaptedLesson>
    {
        public void Configure(EntityTypeBuilder<AdaptedLesson> builder)
        {
            builder.ToTable("AdaptedLessons");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.AdaptationType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.AdaptationVersion)
                .IsRequired();

            builder.Property(a => a.TotalChunks)
                .IsRequired();

            builder.Property(a => a.AdaptedContentJson)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(a => a.PassedSafetyFilter)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(a => a.SafetyFlagReason)
                .HasMaxLength(500);

            builder.Property(a => a.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(a => a.GeneratedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(a => a.ExpiresAt)
                .IsRequired();

            // Filtered unique index: only one active adapted lesson per (student, lesson) pair
            // CREATE UNIQUE INDEX UIX_AdaptedLessons_ActivePerStudentLesson
            //   ON AdaptedLessons(StudentProfileId, StandardLessonId) WHERE IsActive = 1
            builder.HasIndex(a => new { a.StudentProfileId, a.StandardLessonId })
                .IsUnique()
                .HasDatabaseName("UIX_AdaptedLessons_ActivePerStudentLesson")
                .HasFilter("[IsActive] = 1");

            // Relationships
            // AdaptedLessons.StudentProfileId -> StudentProfiles.Id : CASCADE
            builder.HasOne(a => a.StudentProfile)
                .WithMany(s => s.AdaptedLessons)
                .HasForeignKey(a => a.StudentProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // AdaptedLessons.StandardLessonId -> StandardLessons.Id : RESTRICT
            builder.HasOne(a => a.StandardLesson)
                .WithMany(l => l.AdaptedLessons)
                .HasForeignKey(a => a.StandardLessonId)
                .OnDelete(DeleteBehavior.Restrict);

            // LessonMicroChunks.AdaptedLessonId -> AdaptedLessons.Id : CASCADE
            builder.HasMany(a => a.LessonMicroChunks)
                .WithOne(m => m.AdaptedLesson)
                .HasForeignKey(m => m.AdaptedLessonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
