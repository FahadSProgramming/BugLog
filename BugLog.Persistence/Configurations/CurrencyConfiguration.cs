using BugLog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugLog.Persistence.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder) {
            
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.ExchangeRate).IsRequired().HasDefaultValue(1.00M).ValueGeneratedOnAdd();
            builder.Property(p => p.BaseCurrency).IsRequired().HasDefaultValue(false).ValueGeneratedOnAdd();

            builder.HasOne(p => p.Country)
            .WithOne(x => x.Currency)
            .HasForeignKey<Currency>(x => x.CountryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.CreatedBy)
            .WithMany(p => p.CurrencyCreateActions)
            .HasForeignKey(p => p.CreatedById)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.ModifiedBy)
            .WithMany(p => p.CurrencyUpdateActions)
            .HasForeignKey(p => p.ModifiedById)
            .OnDelete(DeleteBehavior.SetNull);

        }
    }
}