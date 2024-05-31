/*using API.Features.Identity.Commands.SignOut;
using API.Features.Identity.Commands.SingIn;*/
using API.Features.Identity.Commands.SingIn;
using API.Features.Identity.Commands.SingUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using API.Attributes;
using API.Common;
using API.Features.Identity.Commands.ChangePassword;
using API.Features.Identity.Commands.ConfirmEmail;
using API.Features.Identity.Commands.ResetPassword;
using API.Features.Identity.Commands.ResetPasswordRequest;
using API.Features.Identity.Commands.SIgnInGoogle;
using API.Features.Identity.Commands.SignUpCompany;
using API.Features.Identity.Entities;
using API.Features.Identity.Models;
using API.Features.Identity.Static;
using Microsoft.AspNetCore.Authorization;
using API.Features.Images.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Newtonsoft.Json;

namespace API.Features.Identity.Controllers
{
    [ApiController]
    [Route("api/user-identity")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<User> _signInManager;

        public AccountController(IMediator mediator, SignInManager<User> signInManager)
        {
            _mediator = mediator;
            _signInManager = signInManager;
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
        
        [HttpGet("signin-google")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GoogleLogin()
        {
            var redirectUrl = Url.Action("GoogleResponse", "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return Challenge(properties, "Google");
        }
    
        [HttpGet("google-response")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<JsonWebToken>> GoogleResponse(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new SignInGoogleCommand(), cancellationToken);
            var sessionId = Guid.NewGuid().ToString();
            
            HttpContext.Session.SetString(sessionId, JsonConvert.SerializeObject(result));
            
            var redirectUrl = $"{Globals.ApplicationUrl}/google-response?sessionId={sessionId}";
            return Redirect(redirectUrl);
        }
        
        [HttpGet("get-session-data/{sessionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<JsonWebToken>> GetSessionData(string sessionId)
        {
            if (HttpContext.Session.TryGetValue(sessionId, out var sessionData))
            {
                var json = System.Text.Encoding.UTF8.GetString(sessionData);
                var result = JsonConvert.DeserializeObject<JsonWebToken>(json);
                
                HttpContext.Session.Remove(sessionId);
                
                return Ok(result);
            }
            return Unauthorized();
        }

    }
}

