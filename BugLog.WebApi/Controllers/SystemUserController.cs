using BugLog.Application.Exceptions;
using BugLog.Application.SystemUsers.Commands;
using BugLog.Application.SystemUsers.Commands.Register;
using BugLog.Application.SystemUsers.Queries;
using BugLog.Application.SystemUsers.Queries.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BugLog.WebApi.Controllers
{
    public class SystemUserController : BaseController
    {

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterSystemUserCommand request, CancellationToken cancellationToken = default) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginSystemUserQuery request, CancellationToken cancellationToken = default) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("Detail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSystemUserDetail([FromBody] GetSystemUserDetailQuery request, CancellationToken cancellationToken = default) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetSystemUserCollection(CancellationToken cancellationToken = default) {
            var response = await Mediator.Send(new GetSystemUserListQuery(), cancellationToken);
            return Ok(response);
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSystemUser([FromBody] UpdateSystemUserCommand request, CancellationToken cancellationToken) {
            await Mediator.Send(request, cancellationToken);
            return NoContent();
        }

    }
}