using API.Database.Context;
using API.Features.Identity.Entities;
using API.Features.Identity.Exeptions;
using API.Features.Identity.Models;
using API.Features.Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using Amazon.Runtime;
using API.Features.Identity.Exceptions;

namespace API.Features.Identity.Commands.SingIn
{
    public sealed class SingInHandler : IRequestHandler<SingInCommand, JsonWebToken>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly EFContext _context;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;

        // Konstruktor do wstrzykiwania zależności.
        public SingInHandler(SignInManager<User> signInManager, EFContext context, ITokenService tokenService, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _context = context;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        public async Task<JsonWebToken> Handle(SingInCommand request, CancellationToken cancellationToken)
        {
            //sprawdzenie uzytkownika czy jest w bazie
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email)
                ?? throw new InvalidCredentials(); // Rzuca wyjątek, jeśli użytkownik nie zostanie znaleziony.
            //sprawdzenie poprawności hasła
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
            // Jeśli sprawdzanie hasła nie powiedzie się, rzuca wyjątek.
            if (!result.Succeeded)
                throw new SignInException(result);

            // Pobiera role i oświadczenia użytkownika.
            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);
            // generowanie JSON Web Token dla uzytkownika
            var jwt = _tokenService.GenerateAccessTokenAsync(user.Id, user.Email, userRoles, userClaims);

            return jwt;
        }
    }
}
