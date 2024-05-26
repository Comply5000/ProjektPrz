using FluentValidation;

namespace API.Features.Identity.Commands.ChangePassword;

public sealed class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Stare hasło jest wymagane");
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Nowe hasło jest wymagane");
        RuleFor(x => x.NewConfirmedPassword).NotEmpty().WithMessage("Powtórzone hasło jest wymagane");
        RuleFor(x => x.NewConfirmedPassword).Equal(x => x.NewPassword).WithMessage("Powtórzone hasło musi być takie samo");
    }
}