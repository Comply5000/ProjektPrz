using FluentValidation;

namespace API.Features.Identity.Commands.SingIn
{
    public class SingInValidator : AbstractValidator<SingInCommand>
    {
        //wprowadzenie reguł walidacji dla SingIn feature
        public SingInValidator()
        {
            //pole email i hasło nie może być puste
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
