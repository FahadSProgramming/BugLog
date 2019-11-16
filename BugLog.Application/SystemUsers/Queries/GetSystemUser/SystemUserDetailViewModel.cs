using System;

namespace BugLog.Application.SystemUsers.Queries
{
    public class SystemUserDetailViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public bool IsVerified { get; set; }
        public Guid? UserManagerId { get; set; }
    }
}