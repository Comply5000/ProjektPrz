using API.Common.Responses;
using MediatR;
using System.Xml.Serialization;

namespace API.Features.Offers.Commands.UpdateOffer
{
    public sealed record UpdateOfferCommand(
        string Name,
        string Description,
        IFormFile Image) : IRequest<CreateOrUpdateResponse>
    {
        internal Guid Id { get; set; }
    }
}
