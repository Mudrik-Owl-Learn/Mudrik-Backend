using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudrik.Domain.Entities;

namespace Mudrik.Domain.Configurations
{
    public class BadgeConfiguration : IEntityTypeConfiguration<Badge>
    {
        public void Configure(EntityTypeBuilder<Badge> builder)
        {
            builder.ToTable("Badges");

            builder.HasKey(b => b.Id); 

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(b => b.Description)
                .HasMaxLength(500);

            builder.Property(b => b.Rarity)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.ImageUrl)
                .HasMaxLength(500);

            builder.Property(b => b.EligibilityCriteriaJson)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(b => b.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Relationships
            // StudentBadges.BadgeId -> Badges.Id : RESTRICT
            builder.HasMany(b => b.StudentBadges)
                .WithOne(sb => sb.Badge)
                .HasForeignKey(sb => sb.BadgeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
