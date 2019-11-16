using System.Text;
using System.Security.Cryptography;
using BugLog.Application.Interfaces;

namespace BugLog.Services.Security
{
    public class PasswordGenerationService : IPasswordGenerationService
    {
        public void GeneratePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512()) {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.ASCII.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt)) {
                var computedHash = hmac.ComputeHash(Encoding.ASCII.GetBytes(password));
                for(var i = 0; i < computedHash.Length; i++) {
                    if(computedHash[i] != passwordHash[i]) {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}