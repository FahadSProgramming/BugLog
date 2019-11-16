using System;
using System.Collections.Generic;

namespace BugLog.Domain.Entities {
    public class Country : BaseEntity {
        public string Name { get; set; }
        public string TwoDigitISOCode { get; set; }
        public Currency Currency { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
}