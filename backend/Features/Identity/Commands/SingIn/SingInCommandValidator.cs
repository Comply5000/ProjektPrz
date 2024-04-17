using FluentValidation;

namespace API.Features.Identity.Commands.SingIn
{
    public class SingInCommandValidator : AbstractValidator<SingInCommand>
    {
        public SingInCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        }
    }
}
