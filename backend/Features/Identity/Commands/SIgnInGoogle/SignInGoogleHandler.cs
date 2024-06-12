using System.Security.Claims;
using API.Features.Identity.Entities;
using API.Features.Identity.Exceptions;
using API.Features.Identity.Exeptions;
using API.Features.Identity.Models;
using API.Features.Identity.Services;
using API.Features.Identity.Static;
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
        
        if (result.Succeeded)
        {
            return await SignIn(info, cancellationToken);
        }

        var user = new User
        {
            Email = info.Principal.FindFirst(ClaimTypes.Email)?.Value,
            UserName = info.Principal.FindFirst(ClaimTypes.Email)?.Value,
            EmailConfirmed = true
        };
        
        var createResult = await _userManager.CreateAsync(user);
        if (!createResult.Succeeded) 
            throw new CreateUserException(createResult.Errors);
        
        var addRoleResult = await _userManager.AddToRoleAsync(user, UserRoles.User);
        if (!addRoleResult.Succeeded)
            throw new AddUserToRoleExeption();

        var addEmailClaimResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
        if (!addEmailClaimResult.Succeeded)
            throw new AddClaimException();
        
        var addNameClaimResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        if (!addNameClaimResult.Succeeded)
            throw new AddClaimException();
        
        var addLoginResult = await _userManager.AddLoginAsync(user, info);
        if (!addLoginResult.Succeeded)
            throw new CreateUserException(addLoginResult.Errors);
        
        await _signInManager.RefreshSignInAsync(user);
        
        var userRoles = await _userManager.GetRolesAsync(user);
        var userClaims = await _userManager.GetClaimsAsync(user);
            
        var token = _tokenService.GenerateAccessTokenAsync(user.Id, user.Email, userRoles, userClaims);
        token.IsExternal = true;
        
        return token;
    }

    private async Task<JsonWebToken> SignIn(ExternalLoginInfo info, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == info.Principal.FindFirst(ClaimTypes.Email).Value, cancellationToken)
                   ?? throw new UserNotFoundExeption();

        var userRoles = await _userManager.GetRolesAsync(user);
        var userClaims = await _userManager.GetClaimsAsync(user);
            
        var token = _tokenService.GenerateAccessTokenAsync(user.Id, user.Email, userRoles, userClaims);
        token.IsExternal = true;
            

        return token;
    }
}