using System;
using System.Collections.Generic;

namespace BugLog.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Category { get; set; }
        public bool IsActive { get; set; }
        public Country Country { get; set; }
        public Guid? CountryId { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<Case> Cases { get; set; }
        public ICollection<ServiceContract> ServiceContracts { get; set; }
    }
}