using System;
using System.Collections.Generic;

namespace BugLog.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string MobilePhone { get; set; }
        public bool IsActive { get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
        public Country Country { get; set; }
        public Guid? CountryId { get; set; }
        public ICollection<Case> Cases { get; set; }
    }
}