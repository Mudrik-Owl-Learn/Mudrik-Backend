using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudrik.Domain.Models;

namespace Mudrik.Domain.Configurations
{
    public class ParentProfileConfiguration : IEntityTypeConfiguration<ParentProfile>
    {
        public void Configure(EntityTypeBuilder<ParentProfile> builder)
        {
            builder.ToTable("ParentProfiles");

            builder.HasKey(p => p.Id);

            builder.Property(a => a.Id)
    .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(p => p.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.ConsentGiven)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasIndex(p => p.UserId)
                .IsUnique();

            // Relationships
            // ParentProfiles.UserId -> Users.Id : CASCADE
            builder.HasOne(p => p.User)
                .WithOne(u => u.ParentProfile)
                .HasForeignKey<ParentProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.StudentProfiles)
                .WithOne(s => s.ParentProfile)
                .HasForeignKey(s => s.ParentProfileId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
