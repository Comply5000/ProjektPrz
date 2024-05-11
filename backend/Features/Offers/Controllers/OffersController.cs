using API.Features.Companies.Queries.GetCompanies;
using API.Features.Offers.Commands.CreateOffer;
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
            var result = _mediator.Send(command);
            return Ok(result);
            //TODO
            //fix this
            //add await
        }


        //Update Offer
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOffer([FromForm] UpdateOfferCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        //Get Offer paginated list
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPaginatedListOffers([FromQuery] GetOfferQuery query)
        {
            var result = _mediator.Send(query);
            return Ok(result);
            //same probles as with create offer
        }
        
        
        //Delete Offer
        
    }
}
