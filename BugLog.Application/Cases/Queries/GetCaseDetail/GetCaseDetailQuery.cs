using System;
using MediatR;
namespace BugLog.Application.Cases.Queries
{
    public class GetCaseDetailQuery : IRequest<CaseDetailViewModel>
    {
        public Guid Id { get; set; }
    }
}