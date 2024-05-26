using API.Features.Identity.Entities;
using API.Features.Identity.Exceptions;
using API.Features.Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Identity.Commands.ChangePassword;

public sealed class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ICurrentUserService _currentUserService;

    public ChangePasswordHandler(UserManager<User> userManager, ICurrentUserService currentUserService)
    {
        _userManager = userManager;
        _currentUserService = currentUserService;
    }
    
    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId,
                cancellationToken)
            ?? throw new UserNotFoundExeption();

        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);

        if (isPasswordCorrect is false)
            throw new InvalidPasswordException();
        
        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
            throw new ChangePasswordException(result.Errors);

    }
}