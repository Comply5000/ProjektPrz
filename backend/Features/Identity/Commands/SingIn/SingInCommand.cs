using API.Features.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
namespace API.Features.Identity.Commands.SingIn
{
    public sealed record SingInCommand (
        string Email,
        string Password) : IRequest<JsonWebToken>;
}
