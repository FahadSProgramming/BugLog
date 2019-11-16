using System;
using FluentValidation;

namespace BugLog.Application.PriceLists.Commands
{
    public class DeletePriceListCommandValidataor : AbstractValidator<DeletePriceListCommand>
    {
        public DeletePriceListCommandValidataor() {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}