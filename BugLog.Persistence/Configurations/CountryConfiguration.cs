using BugLog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugLog.Persistence.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder) {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.TwoDigitISOCode).HasMaxLength(2).IsRequired();

            builder.HasOne(p => p.CreatedBy)
            .WithMany(p => p.CountryCreateActions)
            .HasForeignKey(p => p.CreatedById)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.ModifiedBy)
            .WithMany(p => p.CountryUpdateActions)
            .HasForeignKey(p => p.ModifiedById)
            .OnDelete(DeleteBehavior.SetNull);
        }
    }
}