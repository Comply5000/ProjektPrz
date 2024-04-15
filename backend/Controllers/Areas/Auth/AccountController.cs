/*using API.Features.Identity.Commands.SignOut;
using API.Features.Identity.Commands.SingIn;*/
using API.Features.Identity.Commands.SingUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Areas.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //SingUp
        [HttpPost("sign-up")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp(SingUpCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        //SingIn
        /*[HttpPost("signin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn(SingInCommand command)
        {
            var result = await _mediator.Send(command);
            return Unauthorized();
        }*/
        //SingOut
        /*[HttpPost("signout")]
        public async Task<IActionResult> SignOut()
        {
            await _mediator.Send(new SingOutCommand());
            return Ok("Signed out successfully.");
        }*/
    }
}

