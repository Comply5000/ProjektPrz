using MediatR;

namespace API.Features.Identity.Commands.ConfirmEmail;

public sealed record ConfirmAccountCommand(string Token, Guid UserId) : IRequest;
