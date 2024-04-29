using MediatR;

namespace API.Features.Identity.Commands.ResetPasswordRequest;

public sealed record ResetPasswordRequestCommand(string Email) : IRequest;
