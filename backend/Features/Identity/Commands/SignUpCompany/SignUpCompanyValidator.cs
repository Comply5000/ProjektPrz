using System.Data;
using FluentValidation;

namespace API.Features.Identity.Commands.SignUpCompany;

public sealed class SignUpCompanyValidator : AbstractValidator<SignUpCompanyCommand>
{
    public SignUpCompanyValidator()
    {
        RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Nazwa firmy jest wymagana");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email jest wymagany");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Nieprawidłowy adres email");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Hasło jest wymagane");
        RuleFor(x => x.ConfirmedPassword).NotEmpty().WithMessage("Powtórzone hasło jest wymagane");
        RuleFor(x => x.ConfirmedPassword).Equal(x => x.Password).WithMessage("Powtórzone hasło musi być takie samo");
    }
}