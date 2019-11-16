using System;
using FluentValidation;

namespace BugLog.Application.Products.Commands
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator() {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.Name).MaximumLength(250);
            RuleFor(x => x.DefaultPriceListId).NotEqual(Guid.Empty);
        }
    }
}