using API.Attributes;
using API.Features.Identity.Static;
using API.Features.Comments.Queries.GetComment;
using Microsoft.AspNetCore.Mvc;
using API.Features.Companies.Queries.GetCompanies;
using API.Features.Comments.Commands.CreateComment;
using MediatR;
using Microsoft.AspNetCore.Http;
using API.Features.Comments.Controllers;
using API.Features.Comments.Commands.DeleteComment;


namespace API.Features.Comments.Controllers;

[ApiController]
[Route("api/comments")]
public class CommentsController : ControllerBase
{
    private readonly IMediator _mediator;
    public CommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //Create Comment
    [HttpPost("{offerId:guid}")]
    [ApiAuthorize(UserRoles.User)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateComment([FromForm] CreateCommentCommand command, [FromRoute] Guid offerId)
    {
        command.OfferId = offerId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    //Get Comment
    [HttpGet("{offerId:guid}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetComments([FromRoute] Guid offerId)
    {
        var result = await _mediator.Send(new GetCommentQuery(offerId));
        return Ok(result);
    }

    //Delete Commment
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteComment([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteCommentCommand(id));
        return Ok();
    }
}

