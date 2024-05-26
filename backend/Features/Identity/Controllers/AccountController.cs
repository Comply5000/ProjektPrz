/*using API.Features.Identity.Commands.SignOut;
using API.Features.Identity.Commands.SingIn;*/
using API.Features.Identity.Commands.SingIn;
using API.Features.Identity.Commands.SingUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using API.Attributes;
using API.Features.Identity.Commands.ChangePassword;
using API.Features.Identity.Commands.ConfirmEmail;
using API.Features.Identity.Commands.ResetPassword;
using API.Features.Identity.Commands.ResetPasswordRequest;
using API.Features.Identity.Commands.SignUpCompany;
using API.Features.Identity.Static;
using Microsoft.AspNetCore.Authorization;
using API.Features.Images.Commands;
using Microsoft.AspNetCore.Identity.Data;

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
        public async Task<IActionResult> SignUpCompany(SignUpCompanyCommand command)
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
        
        //ConfirmAccount
        [HttpPost("confirm-account")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmAccount(ConfirmAccountCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }
        
        //ResetPasswordRequest
        [HttpPost("reset-password-request")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPasswordRequest(ResetPasswordRequestCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }
        
        //ResetPassword
        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }
        
        //ResetPassword
        [HttpPost("change-password")]
        [ApiAuthorize(UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}

