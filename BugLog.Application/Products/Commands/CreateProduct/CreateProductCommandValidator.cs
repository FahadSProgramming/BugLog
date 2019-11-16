using System;
using FluentValidation;

namespace BugLog.Application.Products.Commands
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand> {
        public CreateProductCommandValidator() {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(250);
            RuleFor(x => x.DefaultPriceListId).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.ListPrice).NotNull().NotEmpty().GreaterThanOrEqualTo(0);
        }
    }
}