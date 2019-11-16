using System;
using FluentValidation;

namespace BugLog.Application.PriceLists.Commands
{
    public class CreatePriceListCommandValidator : AbstractValidator<CreatePriceListCommand>
    {
        public CreatePriceListCommandValidator() {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.CurrencyId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}