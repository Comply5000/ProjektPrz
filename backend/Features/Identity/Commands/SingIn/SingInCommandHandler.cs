/*using API.Features.Identity.Exeptions;
using MediatR;
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

        public async Task<SignInResult> Handle(SingInCommand request, CancellationToken cancellationToken)
        {
            //sprawdzenie czy user jest w bazie
            var isUserExists = await _signInManager.UserManager.FindByEmailAsync(request.Email);
            if (isUserExists == null)
            {
                throw new UserNotFoundExeption();
            }
            //sprawdzenie czy hasło jest poprawne
            var signInResult = await _signInManager.PasswordSignInAsync(isUserExists, request.Password, false, false);
            if (!signInResult.Succeeded)
            {
                throw new UserPasswordExeption();
            }
            //sprawa tokenu JWT
            return signInResult;
        }
    }
}
*/