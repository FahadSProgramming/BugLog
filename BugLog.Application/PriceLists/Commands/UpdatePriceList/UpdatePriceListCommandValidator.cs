using System;
using FluentValidation;

namespace BugLog.Application.PriceLists.Commands
{
    public class UpdatePriceListCommandValidator : AbstractValidator<UpdatePriceListCommand>
    {
        public UpdatePriceListCommandValidator() {
            RuleFor(x => x.Name).MaximumLength(50);
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.CurrencyId).NotEqual(Guid.Empty);
        }
    }
}