using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Application.Interfaces;
using BugLog.Application.Customers.Commands;
using BugLog.Application.Customers.Queries;
using MediatR;

namespace BugLog.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _midiator;

        public CustomerController(IMediator mediator) {
            _midiator = mediator;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand request, CancellationToken cancellationToken) {
            var response = await _midiator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetCustomerDetail")]
        public async Task<IActionResult> GetCustomerDetail([FromBody] GetCustomerDetailQuery request, CancellationToken cancellationToken) {
            var response = await _midiator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetCustomers(CancellationToken cancellationToken) {
            var response = await _midiator.Send(new GetCustomerListQuery(), cancellationToken);
            return Ok(response);
        }
    }
}