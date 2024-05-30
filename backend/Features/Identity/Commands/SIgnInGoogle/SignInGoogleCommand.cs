using API.Features.Identity.Models;
using MediatR;

namespace API.Features.Identity.Commands.SIgnInGoogle;

public sealed record SignInGoogleCommand : IRequest<JsonWebToken>;
