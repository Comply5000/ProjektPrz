using MediatR;

namespace API.Features.Identity.Commands.ResetPassword;

public sealed record ResetPasswordCommand(string Token, Guid UserId, string Password, string ConfirmedPassword) : IRequest;
