using System;
using FluentValidation;

namespace BugLog.Application.Cases.Queries
{
    public class GetCaseDetailQueryValidator : AbstractValidator<GetCaseDetailQuery>
    {
        public GetCaseDetailQueryValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}