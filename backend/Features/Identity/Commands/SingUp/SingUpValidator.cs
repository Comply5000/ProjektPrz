using FluentValidation;

namespace API.Features.Identity.Commands.SingUp
{
    public class SingUpValidator:AbstractValidator<SingUpCommand>
    {
        public SingUpValidator() {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email address");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.ConfirmedPassword).NotEmpty().WithMessage("Confirmed password is required");
            RuleFor(x => x.ConfirmedPassword).Equal(x => x.Password).WithMessage("Confirmed password must by the same");
        }
    }
}
