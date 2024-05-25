using FluentValidation;

namespace API.Features.Offers.Commands.UpdateOffer
{
    public class UpdateOfferValidator:AbstractValidator<UpdateOfferCommand>
    {
        public UpdateOfferValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nazwa jest wymagana");
        }
    }
}
