using System;
using FluentValidation;

namespace BugLog.Application.Countries.Commands
{
    public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
    {
        public CreateCountryCommandValidator() {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.TwoDigitISOCode).NotNull().NotEmpty().MaximumLength(2);
        }
    }
}