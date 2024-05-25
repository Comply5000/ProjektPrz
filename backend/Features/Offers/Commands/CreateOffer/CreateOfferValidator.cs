using FluentValidation;

namespace API.Features.Offers.Commands.CreateOffer
{
    public class CreateOfferValidator:AbstractValidator<CreateOfferCommand>
    {
        public CreateOfferValidator() { 
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Nazwa jest wymagana");
        }
    }
}
