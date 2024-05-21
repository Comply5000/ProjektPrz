using API.Common.Responses;
using API.Features.Companies.Entities;
using MassTransit.Futures.Contracts;
using MediatR;

namespace API.Features.Offers.Commands.CreateOffer;

public record class CreateOfferCommand(
    string Name,
    string Description,
    DateTimeOffset DateFrom,
    DateTimeOffset DateTo,
    IFormFile Image,
    int Type
    ) : IRequest<CreateOrUpdateResponse>;