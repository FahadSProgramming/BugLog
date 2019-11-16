using System;
using System.Collections.Generic;

namespace BugLog.Domain.Entities
{
    public class SystemUser : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public SystemUser UserManager { get; set; }
        public Guid? UserManagerId { get; set; }
        public ICollection<SystemUser> Subordinates { get; set; }
        public ICollection<SystemUser> CreateActions { get; set; }
        public ICollection<SystemUser> UpdateActions { get; set; }
        public ICollection<TaxProfile> TaxProfileCreateActions { get; set; }
        public ICollection<TaxProfile> TaxProfileUpdateActions { get; set; }
        public ICollection<ServiceContract> ServiceContractCreateActions { get; set; }
        public ICollection<ServiceContract> ServiceContractUpdateActions { get; set; }
        public ICollection<ServiceContractLine> ServiceContractLineCreateActions { get; set; }
        public ICollection<ServiceContractLine> ServiceContractLineUpdateActions { get; set; }
        public ICollection<Product> ProductCreateActions { get; set; }
        public ICollection<Product> ProductUpdateActions { get; set; }
        public ICollection<PriceList> PriceListCreateActions { get; set; }
        public ICollection<PriceList> PriceListUpdateActions { get; set; }
        public ICollection<PriceListItem> PriceListItemCreateActions { get; set; }
        public ICollection<PriceListItem> PriceListItemUpdateActions { get; set; }
        public ICollection<Customer> CustomerCreateActions { get; set; }
        public ICollection<Customer> CustomerUpdateActions { get; set; }
        public ICollection<Contact> ContactCreateActions { get; set; }
        public ICollection<Contact> ContactUpdateActions { get; set; }
        public ICollection<Country> CountryCreateActions { get; set; }
        public ICollection<Country> CountryUpdateActions { get; set; }
        public ICollection<Currency> CurrencyCreateActions { get; set; }
        public ICollection<Currency> CurrencyUpdateActions { get; set; }
        public ICollection<Case> CaseCreateActions { get; set; }
        public ICollection<Case> CaseUpdateActions { get; set; }
    }
}