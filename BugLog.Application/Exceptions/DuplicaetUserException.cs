using System;

namespace BugLog.Application.Exceptions
{
    public class DuplicateUserException : Exception
    {
        public DuplicateUserException(string name, object key, string message) : base($"Operation failed on \"{name}\". ({key}) already exists. Details: {message}") {
        }
         public DuplicateUserException(string name, object key) : base($"Operation failed on \"{name}\". ({key}) already exists.") {
        }
    }
}