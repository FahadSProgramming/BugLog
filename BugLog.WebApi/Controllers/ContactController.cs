using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Application.Contacts.Queries;
using BugLog.Application.Contacts.Commands;

namespace BugLog.WebApi.Controllers
{
    public class ContactController : BaseController
    {

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactCommand request, CancellationToken cancellationToken) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetContactDetail")]
        public async Task<IActionResult> GetContacts([FromBody] GetContactDetailQuery request, CancellationToken cancellationToken) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllContacts(CancellationToken cancellationToken) {
            var response = await Mediator.Send(new GetContactListQuery(), cancellationToken);
            return Ok(response);
        }

    }
}