using System.Collections.Generic;

namespace BugLog.Application.SystemUsers.Queries
{
    public class SystemUserListViewModel
    {
        public ICollection<SystemUserDetailViewModel> SystemUsers { get; set; }
        public int Count { get; set; }
    }
}