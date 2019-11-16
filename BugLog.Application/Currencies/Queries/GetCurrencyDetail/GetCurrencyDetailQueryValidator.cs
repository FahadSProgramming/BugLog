using System;
using FluentValidation;

namespace BugLog.Application.Currencies.Queries
{
    public class GetCurrencyDetailQueryValidator : AbstractValidator<GetCurrencyDetailQuery>
    {
        public GetCurrencyDetailQueryValidator() {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}