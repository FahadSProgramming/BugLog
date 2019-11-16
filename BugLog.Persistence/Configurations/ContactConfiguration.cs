using BugLog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugLog.Persistence.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder) {
            builder.Property(p => p.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.LastName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.IsActive).HasDefaultValue(false).ValueGeneratedOnAdd();
            builder.Property(p => p.EmailAddress).HasMaxLength(200).IsRequired();
            builder.Property(p => p.MobilePhone).HasMaxLength(50);

            builder.HasOne(p => p.Customer)
            .WithMany(p => p.Contacts)
            .HasForeignKey(p => p.CustomerId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

            builder.HasOne(p => p.Country)
            .WithMany(p => p.Contacts)
            .HasForeignKey(p => p.CountryId)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.CreatedBy)
            .WithMany(p => p.ContactCreateActions)
            .HasForeignKey(p => p.CreatedById)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.ModifiedBy)
            .WithMany(p => p.ContactUpdateActions)
            .HasForeignKey(p => p.ModifiedById)
            .OnDelete(DeleteBehavior.SetNull);
        }
    }
}