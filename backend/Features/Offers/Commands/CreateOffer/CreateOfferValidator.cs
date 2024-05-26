using FluentValidation;

namespace API.Features.Offers.Commands.CreateOffer
{
    public class CreateOfferValidator:AbstractValidator<CreateOfferCommand>
    {
        public CreateOfferValidator() { 
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Nazwa jest wymagana");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Opis jest wymagany");
            RuleFor(x=>x.DateTo)
                .NotEmpty()
                .WithMessage("Format daty nieprawidłowy. Poprawny format: YYYY-MM-DDTHH:mm:ss");
            RuleFor(x=>x.DateFrom)
                .NotEmpty()
                .WithMessage("Format daty nieprawidłowy. Poprawny format: YYYY-MM-DDTHH:mm:ss");
            RuleFor(x => x.Type).NotEmpty().WithMessage("Typ jest wymagany");
        }
    }
}
