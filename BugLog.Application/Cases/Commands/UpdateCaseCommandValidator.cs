using System;
using FluentValidation;

namespace BugLog.Application.Cases.Commands
{
    public class UpdateCaseCommandValidator : AbstractValidator<DeleteCaseCommand>
    {
        public UpdateCaseCommandValidator() {
            
        }
    }
}