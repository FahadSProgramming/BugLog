using BugLog.Application.SystemUsers.Commands.Register;
using BugLog.Application.SystemUsers.Queries;
using BugLog.Application.SystemUsers.Queries.Login;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace BugLog.WebApi.Controllers
{
    public class SystemUserController : BaseController
    {

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterSystemUserCommand request, CancellationToken cancellationToken = default) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginSystemUserQuery request, CancellationToken cancellationToken = default) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("Detail")]
        public async Task<IActionResult> GetSystemUserDetail([FromBody] GetSystemUserDetailQuery request, CancellationToken cancellationToken = default) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

    }
}