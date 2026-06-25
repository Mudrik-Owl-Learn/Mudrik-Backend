using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Infrastructure.Configurations
{
    public class AgentGeneratedQuizQuestionConfiguration : IEntityTypeConfiguration<AgentGeneratedQuizQuestion>
    {
        public void Configure(EntityTypeBuilder<AgentGeneratedQuizQuestion> builder)
        {
            builder.ToTable("AgentGeneratedQuizQuestions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Prevent the same question being attached twice to the same quiz attempt
            builder.HasIndex(x => new { x.AgentGeneratedQuizId, x.QuizQuestionId })
                .IsUnique();

            // AgentGeneratedQuizQuestions.AgentGeneratedQuizId -> AgentGeneratedQuizzes.Id : CASCADE
            // If the quiz attempt is deleted, its question assignments go with it.
            builder.HasOne(x => x.AgentGeneratedQuiz)
                .WithMany(q => q.AgentGeneratedQuizQuestions)
                .HasForeignKey(x => x.AgentGeneratedQuizId)
                .OnDelete(DeleteBehavior.Cascade);

            // AgentGeneratedQuizQuestions.QuizQuestionId -> QuizQuestions.Id : RESTRICT
            // Mirrors StudentQuizAnswerConfiguration's RESTRICT on the same question bank —
            // don't allow deleting a question that's still referenced by any quiz assignment.
            builder.HasOne(x => x.QuizQuestion)
                .WithMany(q => q.AgentGeneratedQuizQuestions)
                .HasForeignKey(x => x.QuizQuestionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
