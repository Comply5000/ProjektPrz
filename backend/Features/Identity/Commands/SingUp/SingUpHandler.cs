using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

namespace API.Features.Identity.Commands.SingUp
{
    public class SignUpCommandHandler:IRequestHandler<SingUpCommand>
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;

        public SignUpCommandHandler(IMediator mediator, IIdentityService identityService)
        {
            _mediator = mediator;
            _identityService = identityService;
        }
        public async Task Handle(SingUpCommand request, CancellationToken cancellationToken)
        {
            var user = new IdentityUser { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to create user");
            }

            return Unit.Value;
        }
    }
}
