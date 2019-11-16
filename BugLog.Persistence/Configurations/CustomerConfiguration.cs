using BugLog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugLog.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder) {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(250);
            builder.Property(p => p.IsActive).HasDefaultValue(false).ValueGeneratedOnAdd();
            builder.Property(p => p.Phone).HasMaxLength(50);

            builder.HasOne(p => p.Country)
            .WithMany(p => p.Customers)
            .HasForeignKey(p => p.CountryId)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.CreatedBy)
            .WithMany(p => p.CustomerCreateActions)
            .HasForeignKey(p => p.CreatedById)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.ModifiedBy)
            .WithMany(p => p.CustomerUpdateActions)
            .HasForeignKey(p => p.ModifiedById)
            .OnDelete(DeleteBehavior.SetNull);
        }
    }
}