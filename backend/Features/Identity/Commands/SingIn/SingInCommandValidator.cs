using FluentValidation;

namespace API.Features.Identity.Commands.SingIn
{
    public class SingInCommandValidator : AbstractValidator<SingInCommand>
    {
        //wprowadzenie reguł walidacji dla SingIn feature
        public SingInCommandValidator()
        {
            //pole email i hasło nie może być puste
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        }
    }
}
