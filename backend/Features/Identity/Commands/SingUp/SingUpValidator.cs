using FluentValidation;

namespace API.Features.Identity.Commands.SingUp
{
    public class SingUpValidator:AbstractValidator<SingUpCommand>
    {
        //wprowadzenie reguł walidacji dla SingUp feature
        //pola nie mogą być puste
        public SingUpValidator() {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email jest wymagany");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Nieprawidłowy adres email");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Hasło jest wymagane");
            RuleFor(x => x.ConfirmedPassword).NotEmpty().WithMessage("Powtórzone hasło jest wymagane");
            RuleFor(x => x.ConfirmedPassword).Equal(x => x.Password).WithMessage("Powtórzone hasło musi być takie samo");
        }
    }
}
