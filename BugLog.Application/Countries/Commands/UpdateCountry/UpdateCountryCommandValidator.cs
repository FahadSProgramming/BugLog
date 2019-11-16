using System;
using FluentValidation;

namespace BugLog.Application.Countries.Commands
{
    public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
    {
        public UpdateCountryCommandValidator() {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.TwoDigitISOCode).MaximumLength(2);
        }
    }
}