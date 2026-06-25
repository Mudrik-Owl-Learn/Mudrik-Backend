using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudrik.Domain.Models;

namespace Mudrik.Domain.Configurations
{
    public class AgentGeneratedQuizConfiguration : IEntityTypeConfiguration<AgentGeneratedQuiz>
    {
        public void Configure(EntityTypeBuilder<AgentGeneratedQuiz> builder)
        {
            builder.ToTable("AgentGeneratedQuizzes", t =>
            {
                // CHECK constraint: ScorePercent BETWEEN 0.00 AND 100.00, AttemptNumber >= 1
                t.HasCheckConstraint("CK_AgentGeneratedQuizzes_ScorePercent", "[ScorePercent] BETWEEN 0.00 AND 100.00");
                t.HasCheckConstraint("CK_AgentGeneratedQuizzes_AttemptNumber", "[AttemptNumber] >= 1");
            });

            builder.HasKey(q => q.Id);

            builder.Property(a => a.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(q => q.AttemptNumber)
                .IsRequired();

            builder.Property(q => q.AudioReplayCount)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(q => q.ScorePercent)
                .IsRequired()
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(0m);

            builder.Property(q => q.IsPassed)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(q => q.TotalTimeSeconds)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(q => q.StartedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            // Split into two filtered unique indexes since LessonMicroChunkId is now nullable.
            // SQL Server treats every NULL as distinct in a unique index, so a single
            // combined index would no longer prevent duplicate attempts for lesson-only quizzes.

            // Enforces uniqueness for chunk-based quiz attempts
            builder.HasIndex(q => new { q.StudentProfileId, q.LessonMicroChunkId, q.AttemptNumber })
                .IsUnique()
                .HasFilter("[LessonMicroChunkId] IS NOT NULL");

            // Enforces uniqueness for lesson-only quiz attempts (no chunk)
            builder.HasIndex(q => new { q.StudentProfileId, q.StandardLessonId, q.AttemptNumber })
                .IsUnique()
                .HasFilter("[LessonMicroChunkId] IS NULL");

            // Relationships
            // AgentGeneratedQuizzes.StudentProfileId -> StudentProfiles.Id : CASCADE
            builder.HasOne(q => q.StudentProfile)
                .WithMany(s => s.AgentGeneratedQuizzes)
                .HasForeignKey(q => q.StudentProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // AgentGeneratedQuizzes.LessonMicroChunkId -> LessonMicroChunks.Id : RESTRICT (optional FK)
            builder.HasOne(q => q.LessonMicroChunk)
                .WithMany(m => m.AgentGeneratedQuizzes)
                .HasForeignKey(q => q.LessonMicroChunkId)
                .OnDelete(DeleteBehavior.Restrict);

            // AgentGeneratedQuizzes.StandardLessonId -> StandardLessons.Id : RESTRICT
            builder.HasOne(q => q.StandardLesson)
                .WithMany(l => l.AgentGeneratedQuizzes)
                .HasForeignKey(q => q.StandardLessonId)
                .OnDelete(DeleteBehavior.Restrict);

            // StudentQuizAnswers.AgentGeneratedQuizId -> AgentGeneratedQuizzes.Id : CASCADE
            // Declared from StudentQuizAnswerConfiguration instead — removed here to avoid duplication.
        }
    }
}