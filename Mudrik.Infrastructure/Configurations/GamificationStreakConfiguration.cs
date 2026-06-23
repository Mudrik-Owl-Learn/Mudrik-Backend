using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudrik.Domain.Models;

namespace Mudrik.Domain.Configurations
{
    public class GamificationStreakConfiguration : IEntityTypeConfiguration<GamificationStreak>
    {
        public void Configure(EntityTypeBuilder<GamificationStreak> builder)
        {
            builder.ToTable("GamificationStreaks");

            builder.HasKey(g => g.Id);

            builder.Property(a => a.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(g => g.TotalPoints)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(g => g.CurrentLevel)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(g => g.CurrentStreak)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(g => g.LongestStreak)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(g => g.LastStreakDate)
                .HasColumnType("date");

            builder.Property(g => g.FreezeTokensAvailable)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(g => g.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            // 1:1 with Students
            builder.HasIndex(g => g.StudentProfileId)
                .IsUnique();

            // Relationships
            // GamificationStreaks.StudentProfileId -> StudentProfiles.Id : CASCADE
            builder.HasOne(g => g.StudentProfile)
                .WithOne(s => s.GamificationStreak)
                .HasForeignKey<GamificationStreak>(g => g.StudentProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // XpTransactions.GamificationStreakId -> GamificationStreaks.Id : RESTRICT
            builder.HasMany(g => g.XpTransactions)
                .WithOne(x => x.GamificationStreak)
                .HasForeignKey(x => x.GamificationStreakId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
