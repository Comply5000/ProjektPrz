using System.Data;
using FluentValidation;

namespace API.Features.Identity.Commands.SignUpCompany;

public sealed class SignUpCompanyValidator : AbstractValidator<SignUpCompanyCommand>
{
    public SignUpCompanyValidator()
    {
        RuleFor(x => x.CompanyName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email address");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        RuleFor(x => x.ConfirmedPassword).NotEmpty().WithMessage("Confirmed password is required");
        RuleFor(x => x.ConfirmedPassword).Equal(x => x.Password).WithMessage("Confirmed password must by the same");
    }
}