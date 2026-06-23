using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudrik.Domain.Models;

namespace Mudrik.Domain.Configurations
{
    public class StudentBadgeConfiguration : IEntityTypeConfiguration<StudentBadge>
    {
        public void Configure(EntityTypeBuilder<StudentBadge> builder)
        {
            builder.ToTable("StudentBadges");

            builder.HasKey(sb => sb.Id);

            builder.Property(a => a.Id)
    .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(sb => sb.HasBeenDisplayed)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(sb => sb.EarnedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            // A student can earn each badge exactly once
            builder.HasIndex(sb => new { sb.StudentProfileId, sb.BadgeId })
                .IsUnique();

            // Relationships
            // StudentBadges.StudentProfileId -> StudentProfiles.Id : CASCADE
            builder.HasOne(sb => sb.StudentProfile)
                .WithMany(s => s.StudentBadges)
                .HasForeignKey(sb => sb.StudentProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // StudentBadges.BadgeId -> Badges.Id : RESTRICT
            builder.HasOne(sb => sb.Badge)
                .WithMany(b => b.StudentBadges)
                .HasForeignKey(sb => sb.BadgeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
