using System;

namespace BugLog.Application.Interfaces
{
    public interface ISystemUserAccessorService
    {
        Guid? GetCurrentySystemuUserId();
    }
}