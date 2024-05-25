using API.Attributes;
using API.Features.Companies.Queries.GetCompanies;
using API.Features.Identity.Static;
using API.Features.Offers.Commands.CreateOffer;
using API.Features.Offers.Commands.DeleteOffer;
using API.Features.Offers.Commands.UpdateOffer;
using API.Features.Offers.Queries.GetOffer;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Features.Offers.Controllers
{
    [ApiController]
    [Route("api/offers")]
    public class OffersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OffersController(IMediator mediator, ILogger<OffersController> logger)
        {
            _mediator = mediator;
        }

        //Create Offer
        [HttpPost]
        //based on author?
        [ApiAuthorize(UserRoles.CompanyOwner)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOffer([FromForm] CreateOfferCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        //Update Offer
        [HttpPut("{id:guid}")]
        [ApiAuthorize(UserRoles.CompanyOwner)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOffer([FromForm] UpdateOfferCommand command, [FromRoute] Guid id)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        //Get Offer paginated list
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPaginatedListOffers([FromQuery] GetOfferQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        //Delete Offer
        [HttpDelete("{id:guid}")]
        [ApiAuthorize(UserRoles.CompanyOwner)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteOffer([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteOfferCommand(id));
            return Ok();
        }

    }
}
