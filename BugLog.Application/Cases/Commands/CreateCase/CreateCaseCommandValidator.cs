using System;
using FluentValidation;

namespace BugLog.Application.Cases.Commands
{
    public class CreateCaseCommandValidator : AbstractValidator<CreateCaseCommand>
    {
        public CreateCaseCommandValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(250);
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.Status).NotNull().NotEmpty();
            RuleFor(x => x.Priority).NotNull().NotEmpty();
            RuleFor(x => x.ContactId).NotEqual(Guid.Empty);
            RuleFor(x => x.CustomerId).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.ServiceContractId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}