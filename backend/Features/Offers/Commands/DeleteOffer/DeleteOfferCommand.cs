using API.Common.Responses;
using MediatR;

namespace API.Features.Offers.Commands.DeleteOffer
{
    public sealed record DeleteOfferCommand(Guid Id) : IRequest;
}
