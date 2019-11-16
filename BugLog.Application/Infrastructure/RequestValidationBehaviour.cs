using System.Collections.Generic;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using BugLog.Application.Interfaces;

namespace BugLog.Application.Infrastructure
{
    public class RequestValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {
            var context = new ValidationContext(request);

            var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(res => res.Errors)
            .Where(x => x != null)
            .ToList();

            if(failures.Count > 0) {
                throw new BugLog.Application.Exceptions.ValidationException(failures);
            }
            
            return next();
        }
    }
}