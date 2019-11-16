using System.Collections.Generic;

namespace BugLog.Application.Contacts.Queries
{
    public class ContactListViewModel
    {
        public ICollection<ContactDetailViewModel> Contacts { get; set; }
        public int Count { get; set; }
    }
}