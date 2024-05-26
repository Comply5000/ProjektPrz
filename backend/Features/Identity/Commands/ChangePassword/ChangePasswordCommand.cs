using MediatR;

namespace API.Features.Identity.Commands.ChangePassword;

public sealed record ChangePasswordCommand(
    string CurrentPassword,
    string NewPassword,
    string NewConfirmedPassword) : IRequest;
