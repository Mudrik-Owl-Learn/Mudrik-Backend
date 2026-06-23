using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudrik.Domain.Models;
using Mudrik.Domain.Models;

namespace Mudrik.Domain.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            // Soft delete pattern
            builder.Property(u => u.DeletedAt)
                .IsRequired(false);

            // Global query filter for soft delete
            builder.HasQueryFilter(u => u.DeletedAt == null);

            // Relationships
            builder.HasOne(u => u.ParentProfile)
                .WithOne(p => p.User)
                .HasForeignKey<ParentProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
