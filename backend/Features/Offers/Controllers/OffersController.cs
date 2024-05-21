﻿using API.Attributes;
using API.Features.Companies.Queries.GetCompanies;
using API.Features.Identity.Static;
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

        public OffersController(IMediator mediator, ILogger<OffersController> logger)
        {
            _mediator = mediator;
        }

        //Create Offer
        [HttpPost("create-offer")]
        //based on author?
        //[ApiAuthorize(UserRoles.CompanyOwner)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOffer([FromForm] CreateOfferCommand command)
        {
            
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        //Update Offer
        [HttpPut("update-offer-by-id")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOffer([FromForm] UpdateOfferCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        //Get Offer paginated list
        [HttpGet("get-offers")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPaginatedListOffers([FromQuery] GetOfferQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        //Delete Offer
        /*[HttpDelete("delete-offer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> DeleteOffer()
        {
            //delete endpoint
        }
*/
    }
}
