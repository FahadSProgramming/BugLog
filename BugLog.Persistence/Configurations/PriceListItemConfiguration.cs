using System;
using BugLog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugLog.Persistence.Configurations
{
    public class PriceListItemConfiguration : IEntityTypeConfiguration<PriceListItem>
    {
        public void Configure(EntityTypeBuilder<PriceListItem> builder) {
            
            builder.HasOne(p => p.PriceList)
            .WithMany(x => x.PriceListItems)
            .HasForeignKey(p => p.PriceListId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Product)
            .WithMany(p => p.PriceListItems)
            .IsRequired()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.ItemPrice).IsRequired();

            builder.HasOne(p => p.CreatedBy)
            .WithMany(p => p.PriceListItemCreateActions)
            .HasForeignKey(p => p.CreatedById)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.ModifiedBy)
            .WithMany(p => p.PriceListItemUpdateActions)
            .HasForeignKey(p => p.ModifiedById)
            .OnDelete(DeleteBehavior.SetNull);
        }
    }
}