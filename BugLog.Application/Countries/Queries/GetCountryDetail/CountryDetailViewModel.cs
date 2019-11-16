using System;
using System.Collections.Generic;
using BugLog.Application.Contacts.Queries;
using BugLog.Application.Customers.Queries;

namespace BugLog.Application.Countries.Queries
{
    public class CountryDetailViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TwoDigitISOCode { get; set; }
        public Guid? CurrencyId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public ICollection<CustomerDetailViewModel> Customers { get; set; }
        public ICollection<ContactDetailViewModel> Contacts { get; set; }
    }
}