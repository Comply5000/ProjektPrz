using API.Common.Models;
using API.Features.Companies.Models;
using MediatR;

namespace API.Features.Companies.Queries.GetCompanyById;

public sealed record GetCompanyByIdQuery(Guid CompanyId) : IRequest<CompanyViewModel>;
