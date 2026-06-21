using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudrik.Domain.Entities;

namespace Mudrik.Domain.Configurations
{
    public class LessonMicroChunkConfiguration : IEntityTypeConfiguration<LessonMicroChunk>
    {
        public void Configure(EntityTypeBuilder<LessonMicroChunk> builder)
        {
            builder.ToTable("LessonMicroChunks");

            builder.HasKey(m => m.Id);

            builder.Property(a => a.Id)
    .HasDefaultValueSql("NEWSEQUENTIALID()");   

            builder.Property(m => m.ChunkOrder)
                .IsRequired();

            builder.Property(m => m.Format)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(m => m.ContentText)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(m => m.AudioScriptUrl)
                .HasMaxLength(500);

            builder.Property(m => m.IllustrationUrl)
                .HasMaxLength(500);

            builder.Property(m => m.EstimatedDurationSeconds)
                .IsRequired();

            builder.Property(m => m.IsCompleted)
                .IsRequired()
                .HasDefaultValue(false);

            // Chunk ordering must be unique within an adapted lesson
            builder.HasIndex(m => new { m.AdaptedLessonId, m.ChunkOrder })
                .IsUnique();

            // Relationships
            // LessonMicroChunks.AdaptedLessonId -> AdaptedLessons.Id : CASCADE
            builder.HasOne(m => m.AdaptedLesson)
                .WithMany(a => a.LessonMicroChunks)
                .HasForeignKey(m => m.AdaptedLessonId)
                .OnDelete(DeleteBehavior.Cascade);

            // LessonMicroChunks.StudentProfileId -> StudentProfiles.Id : CASCADE (direct FK for query performance)
            // NOTE: StudentProfiles already cascades to LessonMicroChunks indirectly via
            // AdaptedLessons. SQL Server does not allow two CASCADE paths to the same
            // table, so this direct FK uses ClientCascade (EF Core change-tracker cascade)
            // instead of a database-level ON DELETE CASCADE. See MudrikDbContext remarks.
            builder.HasOne(m => m.StudentProfile)
                .WithMany(s => s.LessonMicroChunks)
                .HasForeignKey(m => m.StudentProfileId)
                .OnDelete(DeleteBehavior.ClientCascade);

            // AgentGeneratedQuizzes.LessonMicroChunkId -> LessonMicroChunks.Id : RESTRICT
            builder.HasMany(m => m.AgentGeneratedQuizzes)
                .WithOne(q => q.LessonMicroChunk)
                .HasForeignKey(q => q.LessonMicroChunkId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
