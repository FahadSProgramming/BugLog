using BugLog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugLog.Persistence.Configurations
{
    public class ServiceContractLineConfiguration : IEntityTypeConfiguration<ServiceContractLine>
    {
        public void Configure(EntityTypeBuilder<ServiceContractLine> builder) {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(250);

            builder.Property(p => p.UnitPrice).IsRequired();
            builder.Property(p => p.NetPrice).IsRequired();
            
            // builder.HasOne(p => p.Product)
            // .WithMany(p => p.ServiceContractLines)
            // .IsRequired()
            // .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.PriceListItem)
            .WithMany(p => p.ServiceContractLines)
            .HasForeignKey(p => p.PriceListItemId)
            .OnDelete(DeleteBehavior.Restrict);
            

            builder.HasOne(p => p.TaxProfile)
            .WithMany(p => p.ServiceContractLines)
            .HasForeignKey(p => p.TaxProfileId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.PriceList)
            .WithMany(p => p.ServiceContractLines)
            .HasForeignKey(p => p.PriceListId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.ServiceContract)
            .WithMany(p => p.ServiceContractLines)
            .HasForeignKey(p => p.ServiceContractId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.CreatedBy)
            .WithMany(p => p.ServiceContractLineCreateActions)
            .HasForeignKey(p => p.CreatedById)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.ModifiedBy)
            .WithMany(p => p.ServiceContractLineUpdateActions)
            .HasForeignKey(p => p.ModifiedById)
            .OnDelete(DeleteBehavior.SetNull);
        }
    }
}