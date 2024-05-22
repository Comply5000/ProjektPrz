using API.Common.Responses;
using API.Features.Companies.Entities;
using API.Features.Offers.Enums;
using MassTransit.Futures.Contracts;
using MediatR;

namespace API.Features.Offers.Commands.CreateOffer;

public record class CreateOfferCommand(
    string Name,
    string Description,
    DateTimeOffset DateFrom, 
    DateTimeOffset DateTo,
    IFormFile Image,
    OfferType Type
    ) : IRequest<CreateOrUpdateResponse>;