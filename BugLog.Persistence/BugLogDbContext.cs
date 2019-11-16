using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Persistence
{
    public class BugLogDbContext : DbContext, IBugLogDbContext
    {
        private readonly ISystemUserAccessorService _systemUserService;
        public BugLogDbContext(DbContextOptions options, ISystemUserAccessorService systemuserService) : base(options)
        {
            _systemUserService = systemuserService;
        }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<TaxProfile> TaxProfiles { get; set; }
        public DbSet<ServiceContract> ServiceContracts { get; set; }
        public DbSet<ServiceContractLine> ServiceContractLines { get; set; }
        public DbSet<PriceListItem> PriceListItems { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken) {
            AddAuditDetails(ChangeTracker);
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.ApplyConfigurationsFromAssembly(typeof(BugLogDbContext).Assembly);
        }

        private void AddAuditDetails(Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker changeTracker) {
            var currentSystemuserId = _systemUserService.GetCurrentySystemuUserId();;
             changeTracker.Entries().Where(e => e.State == EntityState.Added).ToList()
               .ForEach(x => {
                   x.Property("CreatedOn").CurrentValue = System.DateTime.UtcNow;
                   x.Property("ModifiedOn").CurrentValue = System.DateTime.UtcNow;
                   
                   if(currentSystemuserId != null && !x.Property("Id").CurrentValue.Equals(currentSystemuserId)) {
                        x.Property("CreatedById").CurrentValue = currentSystemuserId;
                        x.Property("ModifiedById").CurrentValue = currentSystemuserId;
                   }
               });
            changeTracker.Entries().Where(e => e.State == EntityState.Modified).ToList()
                .ForEach(x => {
                    x.Property("ModifiedOn").CurrentValue = System.DateTime.UtcNow;
                    if(currentSystemuserId != null && !x.Property("Id").CurrentValue.Equals(currentSystemuserId)) {
                        x.Property("ModifiedById").CurrentValue = currentSystemuserId;
                    }
                });
        }
    }
}