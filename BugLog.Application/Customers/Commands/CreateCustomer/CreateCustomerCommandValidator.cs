using System;
using FluentValidation;

namespace BugLog.Application.Customers.Commands
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator() {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(250);
            RuleFor(x => x.Category).NotNull().NotEmpty();
            RuleFor(x => x.Phone).MaximumLength(50);
            RuleFor(x => x.CountryId).NotEqual(Guid.Empty);
        }
    }
}