using MediatR;

namespace API.Features.Offers.Commands;

public static class CreateOffer
{
    public record Command() : IRequest;
}