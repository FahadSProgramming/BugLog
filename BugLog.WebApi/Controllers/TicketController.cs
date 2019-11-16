using System.Threading;
using System.Threading.Tasks;
using BugLog.Application.Cases.Commands;
using BugLog.Application.Cases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BugLog.WebApi.Controllers
{
    public class TicketController : BaseController
    {
        [HttpGet]
        [Route("GetCaseDetail")]
        public async Task<IActionResult> GetCaseById([FromBody] GetCaseDetailQuery request, CancellationToken cancellationToken = default) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetCases")]
        public async Task<IActionResult> GetCases(CancellationToken cancellationToken = default) {
            var response = await Mediator.Send(new GetCaseListQuery(), cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateCase([FromBody] CreateCaseCommand request, CancellationToken cancellationToken = default) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}