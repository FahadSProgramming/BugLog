using System;
using FluentValidation;

namespace BugLog.Application.TaxProfiles.Commands
{
    public class DeleteTaxProfileCommandValidator : AbstractValidator<DeleteTaxProfileCommand>
    {
        public DeleteTaxProfileCommandValidator() {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}