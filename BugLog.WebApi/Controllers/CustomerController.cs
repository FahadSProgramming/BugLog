using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Application.Customers.Commands;
using BugLog.Application.Customers.Queries;

namespace BugLog.WebApi.Controllers
{
    public class CustomerController : BaseController
    {
         [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand request, CancellationToken cancellationToken) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetCustomerDetail")]
        public async Task<IActionResult> GetCustomerDetail([FromBody] GetCustomerDetailQuery request, CancellationToken cancellationToken) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetCustomers(CancellationToken cancellationToken) {
            var response = await Mediator.Send(new GetCustomerListQuery(), cancellationToken);
            return Ok(response);
        }
    }
}