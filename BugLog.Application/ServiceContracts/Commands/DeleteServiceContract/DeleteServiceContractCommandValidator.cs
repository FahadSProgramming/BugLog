using System;
using FluentValidation;

namespace BugLog.Application.ServiceContracts.Commands
{
    public class DeleteServiceContractCommandValidator : AbstractValidator<DeleteServiceContractCommand>
    {
        public DeleteServiceContractCommandValidator() {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}