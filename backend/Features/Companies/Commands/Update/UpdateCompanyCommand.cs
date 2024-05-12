using API.Common.Responses;
using MediatR;

namespace API.Features.Companies.Commands.Update;

public sealed record UpdateCompanyCommand(
    string Name,
    string Description,
    string Localization,
    IFormFile Image) : IRequest<CreateOrUpdateResponse>;
