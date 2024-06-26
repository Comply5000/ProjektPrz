﻿using API.Attributes;
using API.Features.Companies.Commands.AddAndRemoveCompanyFromFavourite;
using API.Features.Companies.Commands.Update;
using API.Features.Companies.Queries.GetAllCompanies;
using API.Features.Companies.Queries.GetCompanies;
using API.Features.Companies.Queries.GetCompanyById;
using API.Features.Companies.Queries.GetCompanyForUpdate;
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
    
    //UpdateCompany
    [HttpPut]
    [ApiAuthorize(UserRoles.CompanyOwner)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUp([FromForm] UpdateCompanyCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    //UpdateCompany
    [HttpGet("update")]
    [ApiAuthorize(UserRoles.CompanyOwner)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUp()
    {
        var result = await _mediator.Send(new GetCompanyForUpdateQuery());

        if (result is null)
            NotFound();
        
        return Ok(result);
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
    
    //Get all Companies list
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllCompaniesQuery(), cancellationToken);
        return Ok(result);
    }
    
    //GetCompanyById
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPaginatedList([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetCompanyByIdQuery(id));
        return Ok(result);
    }
    
    //AddToFavourite
    [HttpPut("{id:guid}/add-to-favourite")]
    [ApiAuthorize(UserRoles.User)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAndRemove([FromRoute] Guid id)
    {
        await _mediator.Send(new AddAndRemoveCompanyFromFavouriteCommand(id));
        return Ok();
    }
}