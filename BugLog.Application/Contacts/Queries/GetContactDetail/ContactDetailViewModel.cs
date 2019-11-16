using System;
using System.Collections.Generic;
using BugLog.Application.Cases.Queries;

namespace BugLog.Application.Contacts.Queries
{
    public class ContactDetailViewModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string MobilePhone { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public ICollection<CaseDetailViewModel> Cases { get; set; }
    }
}