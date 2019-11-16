using BugLog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugLog.Persistence.Configurations
{
    public class TaxProfileConfiguration : IEntityTypeConfiguration<TaxProfile>
    {
        public void Configure(EntityTypeBuilder<TaxProfile> builder) {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Amount).IsRequired();
            builder.Property(p => p.TaxProfileType).IsRequired();
            builder.HasOne(p => p.Currency)
            .WithMany(p => p.TaxProfiles)
            .IsRequired()
            .HasForeignKey(p => p.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(p => p.CreatedBy)
            .WithMany(p => p.TaxProfileCreateActions)
            .HasForeignKey(p => p.CreatedById)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.ModifiedBy)
            .WithMany(p => p.TaxProfileUpdateActions)
            .HasForeignKey(p => p.ModifiedById)
            .OnDelete(DeleteBehavior.SetNull);
        }
    }
}