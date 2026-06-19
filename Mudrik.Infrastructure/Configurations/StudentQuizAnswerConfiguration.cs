using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudrik.Domain.Entities;

namespace Mudrik.Domain.Configurations
{
    public class StudentQuizAnswerConfiguration : IEntityTypeConfiguration<StudentQuizAnswer>
    {
        public void Configure(EntityTypeBuilder<StudentQuizAnswer> builder)
        {
            builder.ToTable("StudentQuizAnswers");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.SelectedOptionId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.IsCorrect)
                .IsRequired();

            builder.Property(a => a.TimeToAnswerMs)
                .IsRequired();

            builder.Property(a => a.AnswerChangeCount)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(a => a.AnsweredAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            // Relationships
            // StudentQuizAnswers.AgentGeneratedQuizId -> AgentGeneratedQuizzes.Id : CASCADE
            builder.HasOne(a => a.AgentGeneratedQuiz)
                .WithMany(q => q.StudentQuizAnswers)
                .HasForeignKey(a => a.AgentGeneratedQuizId)
                .OnDelete(DeleteBehavior.Cascade);

            // StudentQuizAnswers.StudentProfileId -> StudentProfiles.Id : CASCADE
            // NOTE: This creates a second cascade path to StudentProfiles alongside
            // AgentGeneratedQuiz -> StudentProfile -> ... -> StudentQuizAnswers.
            // SQL Server forbids multiple cascade paths to the same table, so this FK
            // is configured as NO ACTION at the database level while remaining a logical
            // CASCADE relationship; orphan cleanup for this path is handled by the
            // cascade already flowing through AgentGeneratedQuizzes, or via an
            // application-level/trigger-based cleanup if StudentProfile is ever deleted
            // independently of its quizzes. See DbContext remarks for details.
            builder.HasOne(a => a.StudentProfile)
                .WithMany(s => s.StudentQuizAnswers)
                .HasForeignKey(a => a.StudentProfileId)
                .OnDelete(DeleteBehavior.ClientCascade);

            // StudentQuizAnswers.QuizQuestionId -> QuizQuestions.Id : RESTRICT
            builder.HasOne(a => a.QuizQuestion)
                .WithMany(q => q.StudentQuizAnswers)
                .HasForeignKey(a => a.QuizQuestionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
