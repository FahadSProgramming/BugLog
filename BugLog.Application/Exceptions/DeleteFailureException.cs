using System;

namespace BugLog.Application.Exceptions
{
    public class DeleteFailureException : Exception
    {
        public DeleteFailureException(string name, object key, string message) : base($"Failed to delete \"{name}\" ({key}). Details: {message}") {

        }
    }
}