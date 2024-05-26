using Amazon.Runtime.Internal;
using API.Common.Models;
using API.Features.Offers.Models;
using MediatR;

namespace API.Features.Offers.Queries.GetOfferById
{
    public sealed record GetOfferByIdQuery(Guid id) : IRequest<OffersListModel>;
}
