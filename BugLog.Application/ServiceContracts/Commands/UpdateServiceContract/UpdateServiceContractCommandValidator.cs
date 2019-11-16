using System;
using FluentValidation;

namespace BugLog.Application.ServiceContracts.Commands
{
    public class UpdateServiceContractCommandValidator : AbstractValidator<UpdateServiceContractCommand>
    {
        public UpdateServiceContractCommandValidator() {
            RuleFor(x => x.Id).NotEmpty().NotNull().NotEqual(Guid.Empty);
            RuleFor(x => x.Name).MaximumLength(250);
            RuleFor(x => x.PriceListId).NotEqual(Guid.Empty);
            RuleFor(x => x.CustomerId).NotEqual(Guid.Empty);
            RuleFor(x => x.TaxProfileId).NotEqual(Guid.Empty);
        }
    }
}