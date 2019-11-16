using BugLog.Application.SystemUsers.Commands.Register;
using BugLog.Application.SystemUsers.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace BugLog.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemUserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SystemUserController(IMediator mediator) {
            _mediator = mediator;
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterSystemUserCommand request, CancellationToken cancellationToken = default) {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginSystemUserQuery request, CancellationToken cancellationToken = default) {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

    }
}