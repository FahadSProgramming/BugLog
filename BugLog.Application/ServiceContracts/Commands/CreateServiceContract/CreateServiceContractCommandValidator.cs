using System;
using FluentValidation;

namespace BugLog.Application.ServiceContracts.Commands
{
    public class CreateServiceContractCommandValidator : AbstractValidator<CreateServiceContractCommand>
    {
        public CreateServiceContractCommandValidator() {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(250);
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0.0D);
            RuleFor(x => x.StartDate).NotNull().NotEmpty().GreaterThanOrEqualTo(Convert.ToDateTime("2010-01-01"));
            RuleFor(x => x.EndDate).NotNull().NotEmpty().GreaterThan(x => x.StartDate);
            RuleFor(x => x.PriceListId).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.CustomerId).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.TaxProfileId).NotEqual(Guid.Empty);
        }
    }
}