using System;
using System.Collections.Generic;

namespace BugLog.Domain.Entities
{
    public class Currency : BaseEntity
    {
        public string Name { get; set; }
        public double ExchangeRate { get; set; }
        public bool BaseCurrency { get; set; }
        public Country Country { get; set; }
        public Guid CountryId { get; set; }
        public ICollection<TaxProfile> TaxProfiles { get; set; }
        public ICollection<PriceList> PriceLists { get; set; }
        
    }
}