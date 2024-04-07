using MediatR;
using Microsoft.AspNetCore.Identity;

namespace API.Features.Identity.Commands.SignOut
{
    public class SingOutHandler:IRequestHandler<SingOutCommand>
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public SingOutHandler(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task Handle(SingOutCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
            //return Unit.Value;
        }
    }
}
