using API.Common.MessageBroker.Services;
using API.Features.Identity.Entities;
using API.Features.Identity.Events.SendResetPasswordEmail;
using API.Features.Identity.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Identity.Commands.ResetPasswordRequest;

public sealed class ResetPasswordRequestHandler : IRequestHandler<ResetPasswordRequestCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IEventBus _eventBus;

    public ResetPasswordRequestHandler(UserManager<User> userManager, IEventBus eventBus)
    {
        _userManager = userManager;
        _eventBus = eventBus;
    }
    
    public async Task Handle(ResetPasswordRequestCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.AsNoTracking()
                       .Where(x => x.Email == request.Email)
                       .FirstOrDefaultAsync(cancellationToken)
                   ?? throw new UserWithEmailDoesntExistException();

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        
        await _eventBus.PublishAsync(new SendResetPasswordEmailEvent
        {
            Email = request.Email,
            Token = token,
            UserId = user.Id
        }, cancellationToken);
    }
}