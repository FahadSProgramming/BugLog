using BugLog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugLog.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder) {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(250);

            builder.Property(p => p.ListPrice).IsRequired();

            builder.HasOne(p => p.DefaultPriceList)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.DefaultPriceListId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.CreatedBy)
            .WithMany(p => p.ProductCreateActions)
            .HasForeignKey(p => p.CreatedById)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.ModifiedBy)
            .WithMany(p => p.ProductUpdateActions)
            .HasForeignKey(p => p.ModifiedById)
            .OnDelete(DeleteBehavior.SetNull);
        }
    }
}