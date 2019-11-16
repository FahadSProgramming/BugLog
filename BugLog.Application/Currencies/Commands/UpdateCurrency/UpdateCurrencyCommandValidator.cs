using System;
using FluentValidation;

namespace BugLog.Application.Currencies.Commands
{
    public class UpdateCurrencyCommandValidator : AbstractValidator<UpdateCurrencyCommand>
    {
        public UpdateCurrencyCommandValidator() {
            RuleFor(x => x.Id).NotEmpty().NotNull().NotEqual(Guid.Empty);
            RuleFor(x => x.CountryId).NotEqual(Guid.Empty);
        }
    }
}