using System;
using FluentValidation;

namespace BugLog.Application.ServiceContractLines.Commands
{
    public class CreateServiceContractLineCommandValidator : AbstractValidator<CreateServiceContractLineCommand>
    {
        public CreateServiceContractLineCommandValidator() {
            RuleFor(x => x.PriceListItemId).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.PriceListId).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.TaxProfileId).NotEqual(Guid.Empty);
            RuleFor(x => x.ServiceContractId).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.Quantity).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Discount).GreaterThan(-1);
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(250);

        }
    }
}