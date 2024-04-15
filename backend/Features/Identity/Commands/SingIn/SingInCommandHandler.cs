/*using MediatR;
using Microsoft.AspNetCore.Identity;

namespace API.Features.Identity.Commands.SingIn
{
    public class SingInCommandHandler : IRequestHandler<SingInCommand, SignInResult>
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public SingInCommandHandler(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult*//**//*> Handle(SingInCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
            return result;
        }
    }
}
*/