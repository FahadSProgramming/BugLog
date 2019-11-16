using System;
using FluentValidation;

namespace BugLog.Application.Countries.Queries
{
    public class GetCountryDetailQueryValidator : AbstractValidator<GetCountryDetailQuery>
    {
        public GetCountryDetailQueryValidator() {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}