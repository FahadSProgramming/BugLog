using BugLog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugLog.Persistence.Configurations
{
    public class ServiceContractConfiguration : IEntityTypeConfiguration<ServiceContract>
    {
        public void Configure(EntityTypeBuilder<ServiceContract> builder) {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(250);
            builder.Property(p => p.Amount).IsRequired();

            builder.Property(p => p.StartDate).IsRequired();
            
            builder.HasOne(p => p.PriceList)
            .WithMany(p => p.ServiceContracts)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.Customer)
            .WithMany(p => p.ServiceContracts)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.TaxProfile)
            .WithMany(p => p.ServiceContracts)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.CreatedBy)
            .WithMany(p => p.ServiceContractCreateActions)
            .HasForeignKey(p => p.CreatedById)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.ModifiedBy)
            .WithMany(p => p.ServiceContractUpdateActions)
            .HasForeignKey(p => p.ModifiedById)
            .OnDelete(DeleteBehavior.SetNull);

        }
    }
}