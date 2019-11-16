using System;
using FluentValidation;

namespace BugLog.Application.TaxProfiles.Commands
{
    public class CreateTaxProfileCommandValidator : AbstractValidator<CreateTaxProfileCommand>
    {
        public CreateTaxProfileCommandValidator() {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(150);
            RuleFor(x => x.Amount).NotNull().NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.CurrencyId).NotEmpty().NotNull().NotEqual(Guid.Empty);
        }
    }
}