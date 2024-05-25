using Microsoft.AspNetCore.Mvc;
using MediatR;
using API.Features.Questions.Commands;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using API.Attributes;
using API.Features.Identity.Static;

namespace API.Features.Questions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionId = await _mediator.Send(command);

            return CreatedAtAction(nameof(CreateQuestion), new { id = questionId }, command);
        }
        [HttpPost("answer")]
        [ApiAuthorize(UserRoles.CompanyOwner)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AnswerQuestion([FromBody] AnswerQuestionCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var answerId = await _mediator.Send(command);

            return Ok(new { id = answerId, answer = command.Answer });
        }

    }

}

