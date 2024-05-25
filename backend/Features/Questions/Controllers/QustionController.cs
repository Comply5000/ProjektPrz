using Microsoft.AspNetCore.Mvc;
using MediatR;
using API.Features.Questions.Commands;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using API.Attributes;
using API.Features.Identity.Static;
using API.Features.Questions.Commands.AnswerQuestion;
using API.Features.Questions.Commands.Create;

namespace API.Features.Questions.Controllers
{
    [ApiController]
    [Route("api/questions")]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{offerId:guid}")]
        [ApiAuthorize(UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionCommand command, [FromRoute] Guid offerId)
        {
            command.OfferId = offerId;
            var response = await _mediator.Send(command);

            return Created(string.Empty, response);
        }
        [HttpPost("{id:guid}/answer")]
        [ApiAuthorize(UserRoles.CompanyOwner)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AnswerQuestion([FromBody] AnswerQuestionCommand command, [FromRoute] Guid id)
        {
            command.QuestionId = id;
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }

}

