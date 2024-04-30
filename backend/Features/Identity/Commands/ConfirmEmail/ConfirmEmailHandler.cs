using API.Database.Context;
using API.Features.Identity.Entities;
using API.Features.Identity.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Identity.Commands.ConfirmEmail;

public sealed class ConfirmAccountHandler : IRequestHandler<ConfirmAccountCommand>
{
    private readonly UserManager<User> _userManager;

    public ConfirmAccountHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task Handle(ConfirmAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken)
                   ?? throw new ConfirmAccountException();

        var result = await _userManager.ConfirmEmailAsync(user, request.Token);
        if (!result.Succeeded)
            throw new ConfirmAccountException();
    }
}