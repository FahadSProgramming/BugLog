using System;
using FluentValidation;

namespace BugLog.Application.Cases.Commands
{
    public class DeleteCaseCommandValidator : AbstractValidator<DeleteCaseCommand>
    {
        public DeleteCaseCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}