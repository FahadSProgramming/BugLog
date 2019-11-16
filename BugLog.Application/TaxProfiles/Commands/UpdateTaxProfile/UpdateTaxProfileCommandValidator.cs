using System;
using FluentValidation;

namespace BugLog.Application.TaxProfiles.Commands
{
    public class UpdateTaxProfileCommandValidator : AbstractValidator<UpdateTaxProfileCommand>
    {
        public UpdateTaxProfileCommandValidator() {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.Name).MaximumLength(150);
            RuleFor(x => x.CurrencyId).NotEqual(Guid.Empty);
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
        }
        
    }
}