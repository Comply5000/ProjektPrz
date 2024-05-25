using MediatR;

namespace API.Features.Companies.Commands.AddAndRemoveCompanyFromFavourite;

public sealed record AddAndRemoveCompanyFromFavouriteCommand(Guid Id) : IRequest;