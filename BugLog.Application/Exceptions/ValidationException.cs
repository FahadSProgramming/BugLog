using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace BugLog.Application.Exceptions
{
    // to be implemented with fluent validation release for .net core 3.0
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation failures occurred.") {

        }
        public ValidationException(string message): base(message) {
        }

        public ValidationException(List<ValidationFailure> failures)
            : this() {
            
            var propertyNames = failures.Select(f => f.PropertyName).Distinct();

            foreach(var property in propertyNames) {

                 var propertyFailures = failures
                    .Where(e => e.PropertyName == property)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(property, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Failures { get; }
    }
}