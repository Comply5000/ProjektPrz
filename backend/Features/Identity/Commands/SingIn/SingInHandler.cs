using API.Database.Context;
using API.Features.Identity.Entities;
using API.Features.Identity.Models;
using API.Features.Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace API.Features.Identity.Commands.SingIn
{
    public sealed class SingInHandler : IRequestHandler<SingInCommand, JsonWebToken>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly EFContext _context;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;

        public SingInHandler(SignInManager<User> signInManager, EFContext context, ITokenService tokenService, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _context = context;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        public async Task<JsonWebToken> Handle(SingInCommand request, CancellationToken cancellationToken)
        {
            //check email
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
                //?? throw new InvalidCredentials();

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            //if (!result.Succeeded)
                //throw new InvalidCredentials();


            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);

            var jwt = _tokenService.GenerateAccessTokenAsync(user.Id, user.Email, userRoles, userClaims);

            return jwt;
        }
    }
}
