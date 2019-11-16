using BugLog.Domain.Entities;

namespace BugLog.Application.Interfaces
{
    public interface IJwtGeneratorService
    {
         string GenerateJwtToken(SystemUser user);
    }
}