using System;
using BugLog.Application.Infrastructure;

namespace BugLog.Application.TaxProfiles.Queries
{
    public class TaxProfileDetailViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public TaxProfileTypeEnum TaxProfileType { get; set; }
        public Guid CurrencyId { get; set; }
    }
}