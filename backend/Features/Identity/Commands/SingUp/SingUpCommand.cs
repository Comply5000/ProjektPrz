using MediatR;

namespace API.Features.Identity.Commands.SingUp
{
    public sealed record SingUpCommand(
        string? UserName,
        string? Email,
        string? Password,
        string? ConfirmedPassword
        ):IRequest;
}
