/*using API.Features.Identity.Commands.SignOut;
using API.Features.Identity.Commands.SingIn;*/
using API.Features.Identity.Commands.SingIn;
using API.Features.Identity.Commands.SingUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using API.Attributes;
using API.Features.Identity.Commands.SignUpCompany;
using API.Features.Identity.Static;
using Microsoft.AspNetCore.Authorization;
using API.Features.Images.Commands;

namespace API.Features.Identity.Controllers
{
    [ApiController]
    [Route("api/user-identity")]
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
            return Created();
        }
        
        //SingUp
        [HttpPost("sign-up-company")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp(SignUpCompanyCommand command)
        {
            await _mediator.Send(command);
            return Created();
        }

        //SingIn
        [HttpPost("sign-in")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn(SingInCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}

