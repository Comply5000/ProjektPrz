using API.Common.Models;
using API.Features.Offers.Enums;
using API.Features.Offers.Models;
using MediatR;

namespace API.Features.Offers.Queries.GetCompanyOffersList;

public record GetCompanyOffersListQuery(
    string Search,
    OfferType? Type) : PaginationRequest, IRequest<PaginatedList<OffersListModel>>;  