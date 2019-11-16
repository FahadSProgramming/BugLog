using System;
using FluentValidation;

namespace BugLog.Application.ServiceContractLines.Commands
{
    public class UpdateServiceContractLineCommandValidator : AbstractValidator<UpdateServiceContractLineCommand>
    {
        public UpdateServiceContractLineCommandValidator() {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.Name).MaximumLength(250);
            RuleFor(x => x.TaxProfileId).NotEqual(Guid.Empty);
            RuleFor(x => x.PriceListItemId).NotEqual(Guid.Empty);
            RuleFor(x => x.PriceListId).NotEqual(Guid.Empty);
        }
    }
}