using System;
using FluentValidation;

namespace BugLog.Application.PriceLists.Queries
{
    public class GetPriceListDetailQueryValidator : AbstractValidator<GetPriceListDetailQuery>
    {
        public GetPriceListDetailQueryValidator() {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}