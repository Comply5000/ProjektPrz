using FluentValidation;

namespace API.Features.Companies.Commands.Update;

public sealed class UpdateCompanyValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Nazwa jest wymagana");
    }
}