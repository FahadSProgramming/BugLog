using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Application.Countries.Commands;
using BugLog.Application.Countries.Queries;
using BugLog.Application.Currencies.Commands;
using BugLog.Application.Currencies.Queries;
using BugLog.Application.TaxProfiles.Commands;
using Microsoft.AspNetCore.Http;
using BugLog.Application.TaxProfiles.Queries;

namespace BugLog.WebApi.Controllers
{
    public class MetadataController : BaseController
    {
        #region ControllerGroup: Country
        [HttpPost]
        [Route("Country/Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateCountry([FromBody] CreateCountryCommand request, CancellationToken cancellationToken) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("Country")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCountryDetail([FromBody] GetCountryDetailQuery request, CancellationToken cancellationToken) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("Country/GetAll")]
        public async Task<ActionResult> GetCountries(CancellationToken cancellationToken) {
            var response = await Mediator.Send(new GetCountryListQuery(), cancellationToken);
            return Ok(response);
        }

        [HttpPut]
        [Route("Country/Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateCountry([FromBody] UpdateCountryCommand request, CancellationToken cancellationToken) {
            var response = await Mediator.Send(request, cancellationToken);
            return NoContent();
        }

        [HttpDelete]
        [Route("Country/Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCountry([FromBody] DeleteCountryCommand request, CancellationToken cancellationToken) {
            var response = await Mediator.Send(request, cancellationToken);
            return NoContent();
        }
        #endregion

        #region ControllerGroup: Currency
        [HttpPost]
        [Route("Currency/Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateCurrency([FromBody] CreateCurrencyCommand request, CancellationToken cancellationToken) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("Currency/Detail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCurrencyDetail([FromBody] GetCurrencyDetailQuery request, CancellationToken cancellationToken) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("Currency/GetAll")]
        public async Task<ActionResult> GetCurrencies(CancellationToken cancellationToken) {
            var response = await Mediator.Send(new GetCurrencyListQuery(), cancellationToken);
            return Ok(response);
        }

        [HttpPut]
        [Route("Currency/Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Updatecurrency([FromBody] UpdateCurrencyCommand request, CancellationToken cancellationToken) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpDelete]
        [Route("Currency/Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCurrency([FromBody] DeleteCurrencyCommand request, CancellationToken cancellationToken) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }
        #endregion

        #region ControllerGroup: TaxProfiles
        [HttpPost]
        [Route("TaxProfile/Create")]
        public async Task<IActionResult> CreateTaxProfile([FromBody] CreateTaxProfileCommand request, CancellationToken cancellationToken = default) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("TaxProfile")]
        public async Task<IActionResult> GetTaxProfileDetail([FromBody] GetTaxProfileDetailQuery request, CancellationToken cancellationToken) {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("TaxProfile/GetAll")]
        public async Task<IActionResult> GetTaxProfileCollection(CancellationToken cancellationToken) {
            var response = await Mediator.Send(new GetTaxProfileListQuery(), cancellationToken);
            return Ok(response);
        }

        [HttpPut]
        [Route("TaxProfile/Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateTaxProfile(UpdateTaxProfileCommand request, CancellationToken cancellationToken) {
            var response = await Mediator.Send(request, cancellationToken);
            return NoContent();
        }

        [HttpDelete]
        [Route("TaxProfile/Delete")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteTaxProfile(DeleteTaxProfileCommand request, CancellationToken cancellationToken) {
            var response = await Mediator.Send(request, cancellationToken);
            return NoContent();
        }
        #endregion

    }
}