/*using API.Features.Identity.Commands.SignOut;
using API.Features.Identity.Commands.SingIn;*/
using API.Features.Identity.Commands.SingIn;
using API.Features.Identity.Commands.SingUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace API.Features.Identity.Controllers
{
    [ApiController]
    [Route("user-identity")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //SingUp
        [HttpPost("sign-up")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp(SingUpCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        //SingIn
        [HttpPost("sign-in")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn(SingInCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            //return Ok(result);
            //await Mediator.Send(command, cancellationToken);
            //return Created(string.Empty, null);
            return Ok(result);
        }

            //SingOut
            /*[HttpPost("sign-out")]
            public async Task<IActionResult> SignOut()
            {
                await _mediator.Send(new SingOutCommand());
                return Ok("Signed out successfully.");
            }*/
    }
}

