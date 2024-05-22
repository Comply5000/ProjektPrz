using API.Common.Responses;
using API.Features.Offers.Enums;
using MediatR;
using System.Xml.Serialization;

namespace API.Features.Offers.Commands.UpdateOffer
{
    public sealed record UpdateOfferCommand(
        string Name,
        string Description,
        DateTimeOffset? DateFrom,
        DateTimeOffset? DateTo,
        IFormFile Image,
        OfferType Type) : IRequest<CreateOrUpdateResponse>
    {
        internal Guid Id { get; set; }
    }
}
