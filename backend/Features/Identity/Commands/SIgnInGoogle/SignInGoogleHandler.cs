using System.Security.Claims;
using API.Features.Identity.Entities;
using API.Features.Identity.Exceptions;
using API.Features.Identity.Exeptions;
using API.Features.Identity.Models;
using API.Features.Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Identity.Commands.SIgnInGoogle;

public sealed class SignInGoogleHandler : IRequestHandler<SignInGoogleCommand, JsonWebToken>
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public SignInGoogleHandler(SignInManager<User> signInManager, UserManager<User> userManager, ITokenService tokenService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenService = tokenService;
    }
    
    public async Task<JsonWebToken> Handle(SignInGoogleCommand request, CancellationToken cancellationToken)
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info is null)
            throw new UserNotFoundExeption();
        
        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
        
        JsonWebToken token;
        
        if (result.Succeeded)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == info.Principal.FindFirst(ClaimTypes.Email).Value, cancellationToken)
                       ?? throw new UserNotFoundExeption();

            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);
            
            token = _tokenService.GenerateAccessTokenAsync(user.Id, user.Email, userRoles, userClaims);
            

            return token;
        }

        var createdUser = new User
        {
            Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
            UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
            EmailConfirmed = true
        };
        
        var createResult = await _userManager.CreateAsync(createdUser);
        if (!createResult.Succeeded) 
            throw new UserNotFoundExeption();
        
        var addEmailClaimResult = await _userManager.AddClaimAsync(createdUser, new Claim(ClaimTypes.Email, createdUser.Email));
        if (!addEmailClaimResult.Succeeded)
            throw new AddClaimException();
        
        var addNameClaimResult = await _userManager.AddClaimAsync(createdUser, new Claim(ClaimTypes.NameIdentifier, createdUser.Id.ToString()));
        if (!addNameClaimResult.Succeeded)
            throw new AddClaimException();
        
        var addLoginResult = await _userManager.AddLoginAsync(createdUser, info);
        if (!addLoginResult.Succeeded)
            throw new UserNotFoundExeption();
        
        await _signInManager.RefreshSignInAsync(createdUser);
        
        var userRoles1 = await _userManager.GetRolesAsync(createdUser);
        var userClaims1 = await _userManager.GetClaimsAsync(createdUser);
            
        token = _tokenService.GenerateAccessTokenAsync(createdUser.Id, createdUser.Email, userRoles1, userClaims1);
        
        return token;
    }
}