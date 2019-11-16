using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.Interfaces
{
    public interface IBugLogDbContext
    {
        DbSet<Case> Cases { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Currency> Currencies { get; set; }
        DbSet<PriceList> PriceLists { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<TaxProfile> TaxProfiles { get; set; }
        DbSet<ServiceContract> ServiceContracts { get; set; }
        DbSet<ServiceContractLine> ServiceContractLines { get; set; }
        DbSet<PriceListItem> PriceListItems { get; set; }
        DbSet<SystemUser> SystemUsers { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}