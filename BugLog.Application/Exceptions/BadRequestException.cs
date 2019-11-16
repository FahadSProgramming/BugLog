using System;

namespace BugLog.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) {
        }
    }
}