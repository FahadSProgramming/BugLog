using System;

namespace BugLog.Application.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException(string message) : base(message) {
        }
        public AuthException(string name, string message) : base($"Authentication failed for \"{name}\". Details: {message}.") {
        }
    }
}