﻿using Amazon.Runtime.Internal;
using API.Common.Models;
using API.Features.Offers.Models;
using MediatR;

namespace API.Features.Offers.Queries.GetOffer
{
    public sealed record GetOfferQuery(string Search):PaginationRequest,IRequest<PaginatedList<OffersListModel>>;
}
