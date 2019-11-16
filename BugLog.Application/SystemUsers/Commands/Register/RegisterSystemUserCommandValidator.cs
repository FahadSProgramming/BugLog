using System;
using FluentValidation;

namespace BugLog.Application.SystemUsers.Commands.Register
{
    public class RegisterSystemUserCommandValidator : AbstractValidator<RegisterSystemUserCommand>
    {
        public RegisterSystemUserCommandValidator() {
            RuleFor(x => x.FirstName).MaximumLength(150).NotNull().NotEmpty();
            RuleFor(x => x.LastName).MaximumLength(150).NotNull().NotEmpty();
            RuleFor(x => x.EmailAddress).NotNull().NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(6).MaximumLength(16); 
            RuleFor(x => x.UserManagerId).NotEqual(Guid.Empty);
        }
    }
}