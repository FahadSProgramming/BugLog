using System;
using FluentValidation;

namespace BugLog.Application.Currencies.Commands
{
    public class CreateCurrencyCommandValidator : AbstractValidator<CreateCurrencyCommand>
    {
        public CreateCurrencyCommandValidator() {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.ExchangeRate).NotNull().NotEmpty();
            RuleFor(x => x.BaseCurrency).NotNull();
            RuleFor(x => x.CountryId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}