using System;
using System.Collections.Generic;
using BugLog.Application.Cases.Queries;
using BugLog.Application.Contacts.Queries;
using BugLog.Application.Infrastructure;

namespace BugLog.Application.Customers.Queries
{
    public class CustomerDetailViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Guid? CountryId { get; set; }
        public CustomerCategoryEnum Category { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public ICollection<ContactDetailViewModel> Contacts { get; set; }
        public ICollection<CaseDetailViewModel> Cases { get; set; }
    }
}