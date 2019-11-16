using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BugLog.Domain.Entities;

namespace BugLog.Persistence.Configurations
{
    public class CaseConfiguration : IEntityTypeConfiguration<Case>
    {
        public void Configure(EntityTypeBuilder<Case> builder)
        {
            builder.Property(p => p.Title).HasMaxLength(250).IsRequired();
            builder.Property(p => p.Description).IsRequired().HasColumnType("ntext");
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.Priority).IsRequired();

            builder.HasOne(p => p.Customer)
            .WithMany(p => p.Cases)
            .HasForeignKey(p => p.CustomerId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

            builder.HasOne(p => p.Contact)
            .WithMany(p => p.Cases)
            .HasForeignKey(p => p.ContactId)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.ServiceContract)
            .WithMany(p => p.Cases)
            .HasForeignKey(p => p.ServiceContractId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.CreatedBy)
            .WithMany(p => p.CaseCreateActions)
            .HasForeignKey(p => p.CreatedById)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.ModifiedBy)
            .WithMany(p => p.CaseUpdateActions)
            .HasForeignKey(p => p.ModifiedById)
            .OnDelete(DeleteBehavior.SetNull);
        }
    }
}