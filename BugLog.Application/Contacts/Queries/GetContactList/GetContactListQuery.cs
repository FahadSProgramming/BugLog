using MediatR;

namespace BugLog.Application.Contacts.Queries
{
    public class GetContactListQuery : IRequest<ContactListViewModel>
    {
    }
}