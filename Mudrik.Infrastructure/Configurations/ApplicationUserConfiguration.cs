using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudrik.Domain.Entities;
using Mudrik.Domain.Models;

namespace Mudrik.Domain.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(512);

            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            // Soft delete pattern
            builder.Property(u => u.DeletedAt)
                .IsRequired(false);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            // Global query filter for soft delete
            builder.HasQueryFilter(u => u.DeletedAt == null);

            // Relationships
            builder.HasOne(u => u.ParentProfile)
                .WithOne(p => p.User)
                .HasForeignKey<ParentProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.StudentProfile)
                .WithOne(s => s.User)
                .HasForeignKey<StudentProfile>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
