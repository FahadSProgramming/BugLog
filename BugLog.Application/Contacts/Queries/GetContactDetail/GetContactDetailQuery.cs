using System;
using MediatR;

namespace BugLog.Application.Contacts.Queries
{
    public class GetContactDetailQuery : IRequest<ContactDetailViewModel>
    {
        public Guid Id { get; set; }
    }
}