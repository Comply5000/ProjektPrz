using API.Attributes;
using API.Features.Companies.Commands.AddCompanyToFavourite;
using API.Features.Companies.Commands.Update;
using API.Features.Companies.Queries.GetCompanies;
using API.Features.Identity.Static;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Features.Companies.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompaniesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
   
    
    //GetCompanies paginated list
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPaginatedList([FromQuery] GetCompaniesQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    //GetCompaniesById
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPaginatedList([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetCompaniesByIdQuery(id));
        return Ok(result);
    }
    //AddToFavourite
    [HttpPut("add_to_favourite")]
    [ApiAuthorize(UserRoles.User)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAndRemove(AddAndRemoveCompanyFromFavouriteCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    //UpdateCompany
    [HttpPut]
    [ApiAuthorize(UserRoles.CompanyOwner)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUp(UpdateCompanyCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

}