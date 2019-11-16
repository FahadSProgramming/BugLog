using System;

namespace BugLog.Application.Currencies.Queries
{
    public class CurrencyDetailViewModel
    {
        public Guid Id { get; set; }
        public double ExchangeRate { get; set; }
        public double BaseCurrency { get; set; }
        public Guid CountryId { get; set; }
    }
}