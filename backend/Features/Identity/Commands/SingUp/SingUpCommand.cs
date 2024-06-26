﻿using MediatR;
using Microsoft.IdentityModel.JsonWebTokens;

namespace API.Features.Identity.Commands.SingUp
{
    public sealed record SingUpCommand(
        string Email,
        string Password,
        string ConfirmedPassword
        ):IRequest;
}
