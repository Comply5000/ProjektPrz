using API.Features.Offers.Commands.CreateOffer;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Features.Offers.Controllers
{
    [ApiController]
    [Route("api/offers")]
    public class OffersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OffersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Create Offer
        [HttpPost]
        //based on author?
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOffer([FromForm] CreateOfferCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
            //TODO
            //fix this
        }
        //Update Offer
        //Get Offer
        //Delete Offer
    }
}
