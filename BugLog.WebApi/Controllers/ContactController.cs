using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Application.Contacts.Queries;
using BugLog.Application.Contacts.Commands;

namespace BugLog.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactCommand request, CancellationToken cancellationToken) {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetContactDetail")]
        public async Task<IActionResult> GetContacts([FromBody] GetContactDetailQuery request, CancellationToken cancellationToken) {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllContacts(CancellationToken cancellationToken) {
            var response = await _mediator.Send(new GetContactListQuery(), cancellationToken);
            return Ok(response);
        }

    }
}