using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudrik.Domain.Entities;

namespace Mudrik.Domain.Configurations
{
    public class QuizQuestionConfiguration : IEntityTypeConfiguration<QuizQuestion>
    {
        public void Configure(EntityTypeBuilder<QuizQuestion> builder)
        {
            builder.ToTable("QuizQuestions");

            builder.HasKey(q => q.Id);

            builder.Property(q => q.QuestionText)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(q => q.Format)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(q => q.OptionsJson)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(q => q.CorrectOptionId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(q => q.ConceptTag)
                .HasMaxLength(100);

            builder.Property(q => q.GeneratedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            // Relationships
            // QuizQuestions.StandardLessonId -> StandardLessons.Id : RESTRICT
            builder.HasOne(q => q.StandardLesson)
                .WithMany(l => l.QuizQuestions)
                .HasForeignKey(q => q.StandardLessonId)
                .OnDelete(DeleteBehavior.Restrict);

            // QuizQuestions.SubjectId -> Subjects.Id : RESTRICT
            builder.HasOne(q => q.Subject)
                .WithMany(s => s.QuizQuestions)
                .HasForeignKey(q => q.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // StudentQuizAnswers.QuizQuestionId -> QuizQuestions.Id : RESTRICT
            builder.HasMany(q => q.StudentQuizAnswers)
                .WithOne(a => a.QuizQuestion)
                .HasForeignKey(a => a.QuizQuestionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
