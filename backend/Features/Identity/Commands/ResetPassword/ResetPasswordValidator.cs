using FluentValidation;

namespace API.Features.Identity.Commands.ResetPassword;

public sealed class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Password).NotEmpty().WithMessage("Hasło jest wymagane");
        RuleFor(x => x.ConfirmedPassword).NotEmpty().WithMessage("Powtórzone hasło jest wymagane");
        RuleFor(x => x.ConfirmedPassword).Equal(x => x.Password).WithMessage("Powtórzone hasło musi być takie samo");
    }
}