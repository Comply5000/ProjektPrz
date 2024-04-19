using MediatR;

namespace API.Features.Identity.Commands.SignUpCompany;

public sealed record SignUpCompanyCommand(
    string Email,
    string CompanyName,
    string Password,
    string ConfirmedPassword) : IRequest;
