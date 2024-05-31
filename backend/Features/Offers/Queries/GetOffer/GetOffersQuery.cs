﻿using Amazon.Runtime.Internal;
using API.Common.Models;
using API.Features.Offers.Enums;
using API.Features.Offers.Models;
using MediatR;

namespace API.Features.Offers.Queries.GetOffer
{
    public sealed record GetOffersQuery(
        string Search,
        OfferType? Type,
        Guid? CompanyId,
        bool IsCompanyFavourite = false) : PaginationRequest, IRequest<PaginatedList<OffersListModel>>;
}
