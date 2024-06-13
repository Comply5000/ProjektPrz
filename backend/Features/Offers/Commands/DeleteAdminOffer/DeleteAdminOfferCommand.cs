using MediatR;

namespace API.Features.Offers.Commands.DeleteAdminOffer
{
    public sealed record DeleteAdminOfferCommand(Guid Id) : IRequest;
}
