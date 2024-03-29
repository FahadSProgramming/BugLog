namespace BugLog.Application.Interfaces
{
    public interface IPasswordGenerationService {
         void GeneratePassword(string password, out byte[] passwordHash, out byte[] passwordSalt);
         bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}