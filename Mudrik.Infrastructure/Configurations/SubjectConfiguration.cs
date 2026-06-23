using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudrik.Domain.Models;

namespace Mudrik.Domain.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Subjects");

            builder.HasKey(s => s.Id);

            builder.Property(a => a.Id)
    .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(s => s.IconUrl)
                .HasMaxLength(500);

            builder.Property(s => s.DisplayOrder)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(s => s.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Relationships
            // StandardLessons.SubjectId -> Subjects.Id : RESTRICT
            builder.HasMany(s => s.StandardLessons)
                .WithOne(l => l.Subject)
                .HasForeignKey(l => l.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // QuizQuestions.SubjectId -> Subjects.Id : RESTRICT
            builder.HasMany(s => s.QuizQuestions)
                .WithOne(q => q.Subject)
                .HasForeignKey(q => q.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
