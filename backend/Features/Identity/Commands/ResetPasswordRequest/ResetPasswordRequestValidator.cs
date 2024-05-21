using FluentValidation;

namespace API.Features.Identity.Commands.ResetPasswordRequest;

public sealed class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequestCommand>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email jest wymagany")
            .EmailAddress().WithMessage("Nieprawidłowy adres email");
    }
}