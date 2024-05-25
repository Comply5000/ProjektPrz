using API.Common.Responses;
using MediatR;

namespace API.Features.Offers.Commands.DeleteOffer
{
    public sealed record DeleteOfferCommand(
        string Name
        ):IRequest<CreateOrUpdateResponse>
    {
        internal Guid Id { get; set; }
    }
}
