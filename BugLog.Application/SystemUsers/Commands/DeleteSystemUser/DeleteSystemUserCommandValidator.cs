using System;
using FluentValidation;

namespace BugLog.Application.SystemUsers.Commands
{
    public class DeleteSystemUserCommandValidator : AbstractValidator<DeleteSystemUserCommand>
    {
        public DeleteSystemUserCommandValidator() {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}