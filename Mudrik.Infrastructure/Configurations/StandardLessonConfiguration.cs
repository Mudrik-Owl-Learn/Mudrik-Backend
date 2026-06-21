using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudrik.Domain.Entities;

namespace Mudrik.Domain.Configurations
{
    public class StandardLessonConfiguration : IEntityTypeConfiguration<StandardLesson>
    {
        public void Configure(EntityTypeBuilder<StandardLesson> builder)
        {
            builder.ToTable("StandardLessons");

            builder.HasKey(l => l.Id);

            builder.Property(a => a.Id)
    .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(l => l.Title)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(l => l.RawContentText)
                .HasColumnType("nvarchar(max)");

            builder.Property(l => l.LearningObjectivesJson)
                .HasColumnType("nvarchar(max)")
                .HasDefaultValue("[]");

            builder.Property(l => l.PrerequisiteIdsJson)
                .HasColumnType("nvarchar(max)")
                .HasDefaultValue("[]");

            builder.Property(l => l.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(l => l.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            // Relationships
            // StandardLessons.SubjectId -> Subjects.Id : RESTRICT
            builder.HasOne(l => l.Subject)
                .WithMany(s => s.StandardLessons)
                .HasForeignKey(l => l.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // QuizQuestions.StandardLessonId -> StandardLessons.Id : RESTRICT
            builder.HasMany(l => l.QuizQuestions)
                .WithOne(q => q.StandardLesson)
                .HasForeignKey(q => q.StandardLessonId)
                .OnDelete(DeleteBehavior.Restrict);

            // AdaptedLessons.StandardLessonId -> StandardLessons.Id : RESTRICT
            builder.HasMany(l => l.AdaptedLessons)
                .WithOne(a => a.StandardLesson)
                .HasForeignKey(a => a.StandardLessonId)
                .OnDelete(DeleteBehavior.Restrict);

            // StudentLessonStates.StandardLessonId -> StandardLessons.Id : RESTRICT
            builder.HasMany(l => l.StudentLessonStates)
                .WithOne(sl => sl.StandardLesson)
                .HasForeignKey(sl => sl.StandardLessonId)
                .OnDelete(DeleteBehavior.Restrict);

            // AgentGeneratedQuizzes.StandardLessonId -> StandardLessons.Id : RESTRICT
            builder.HasMany(l => l.AgentGeneratedQuizzes)
                .WithOne(q => q.StandardLesson)
                .HasForeignKey(q => q.StandardLessonId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
