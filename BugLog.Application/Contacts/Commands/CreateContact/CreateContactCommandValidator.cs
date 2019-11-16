using FluentValidation;
using FluentValidation.Validators;

namespace BugLog.Application.Contacts.Commands
{
    public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
    {
        public CreateContactCommandValidator()
        {
            RuleFor(x => x.FirstName).MaximumLength(50).NotNull().NotEmpty();
            RuleFor(x => x.LastName).MaximumLength(50).NotNull().NotEmpty();
            RuleFor(x => x.EmailAddress).EmailAddress(EmailValidationMode.Net4xRegex).NotNull().NotEmpty();
            RuleFor(x => x.MobilePhone).NotNull().NotEmpty();
        }
    }
}