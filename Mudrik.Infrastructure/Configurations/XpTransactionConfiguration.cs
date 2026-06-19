using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudrik.Domain.Entities;

namespace Mudrik.Domain.Configurations
{
    public class XpTransactionConfiguration : IEntityTypeConfiguration<XpTransaction>
    {
        public void Configure(EntityTypeBuilder<XpTransaction> builder)
        {
            builder.ToTable("XpTransactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EventType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.BaseXp)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(x => x.BonusXp)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(x => x.StreakMultiplier)
                .IsRequired()
                .HasColumnType("decimal(4,2)")
                .HasDefaultValue(1.0m);

            builder.Property(x => x.TotalXpAwarded)
                .IsRequired();

            builder.Property(x => x.ReferenceType)
                .HasMaxLength(50);

            builder.Property(x => x.AwardedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            // Relationships
            // XpTransactions.StudentProfileId -> StudentProfiles.Id : CASCADE
            builder.HasOne(x => x.StudentProfile)
                .WithMany(s => s.XpTransactions)
                .HasForeignKey(x => x.StudentProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // XpTransactions.GamificationStreakId -> GamificationStreaks.Id : RESTRICT
            builder.HasOne(x => x.GamificationStreak)
                .WithMany(g => g.XpTransactions)
                .HasForeignKey(x => x.GamificationStreakId)
                .OnDelete(DeleteBehavior.Restrict);

            // Append-only enforcement (No UPDATE/DELETE) is implemented via an
            // EF Core SaveChanges interceptor — see AppendOnlyInterceptor in the
            // infrastructure layer. Not expressible via Fluent API alone.
        }
    }
}
