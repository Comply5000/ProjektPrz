using API.Features.Offers.Models;
using MediatR;

namespace API.Features.Offers.Queries.GetOfferForUpdate;

public sealed record GetOfferForUpdateQuery(Guid Id) : IRequest<OfferUpdateModel>;
