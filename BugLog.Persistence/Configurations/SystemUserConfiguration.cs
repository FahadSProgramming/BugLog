using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BugLog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Persistence.Configurations
{
    public class SystemUserConfiguration : IEntityTypeConfiguration<SystemUser>
    {
        public void Configure(EntityTypeBuilder<SystemUser> builder) {
            
            builder.Property(p => p.FirstName).HasMaxLength(150).IsRequired();
            builder.Property(p => p.LastName).HasMaxLength(150).IsRequired();
            builder.Property(p => p.EmailAddress).IsRequired();
            builder.Property(p => p.PasswordHash).IsRequired();
            builder.Property(p => p.PasswordSalt).IsRequired();
            builder.Property(p => p.IsVerified).IsRequired().HasDefaultValue(false).ValueGeneratedOnAdd();
            builder.Property(p => p.IsActive).IsRequired().HasDefaultValue(true).ValueGeneratedOnAdd();
            builder.Property(p => p.IsLocked).IsRequired().HasDefaultValue(false).ValueGeneratedOnAdd();
            
            builder.HasOne(p => p.UserManager)
            .WithMany(p => p.Subordinates)
            .HasForeignKey(p => p.UserManagerId)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.CreateActions)
            .WithOne(p => p.CreatedBy)
            .HasForeignKey(p => p.CreatedById)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.UpdateActions)
            .WithOne(p => p.ModifiedBy)
            .HasForeignKey(p => p.ModifiedById)
            .OnDelete(DeleteBehavior.SetNull);
        }
    }
}