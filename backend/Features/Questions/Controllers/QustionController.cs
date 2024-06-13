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
using API.Features.Questions.Commands.DeleteAdminQuestion;
using API.Features.Questions.Commands.DeleteQuestion;
using API.Features.Questions.Queries.GetQuestion;

namespace API.Features.Questions.Controllers
{
    [ApiController]
    [Route("api/questions")]
    
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

        //Delete Question
        [HttpDelete("{id:guid}")]
        [ApiAuthorize(UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteQuestion([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteQuestionCommand(id));
            return Ok();
        }
        
        //Delete Question
        [HttpDelete("{id:guid}/admin")]
        [ApiAuthorize(UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAdminQuestion([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteAdminQuestionCommand(id));
            return Ok();
        }
        
        //Get Question
        [HttpGet("{offerId:guid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetQuestions([FromRoute] Guid offerId)
        {
            var result = await _mediator.Send(new GetQuestionsQuery(offerId));
            return Ok(result);
        }

    }

}

