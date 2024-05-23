using MediatR;

    namespace API.Features.Companies.Commands.AddCompanyToFavourite
{
    public sealed record AddAndRemoveCompanyFromFavouriteCommand(Guid Id): IRequest;
    
}
