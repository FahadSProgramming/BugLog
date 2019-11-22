using System;
using FluentValidation;

namespace BugLog.Application.SystemUsers.Commands
{
    public class UpdateSystemUserCommandValidator : AbstractValidator<UpdateSystemUserCommand> {
        public UpdateSystemUserCommandValidator() {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.FirstName).MaximumLength(150);
            RuleFor(x => x.LastName).MaximumLength(150);
            RuleFor(x => x.EmailAddress).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(x => x.UserManagerId).NotEqual(Guid.Empty);
        }
    }
}