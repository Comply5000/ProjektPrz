using API.Features.Companies.Models;
using MediatR;

namespace API.Features.Companies.Queries.GetCompanyForUpdate;

public sealed record GetCompanyForUpdateQuery : IRequest<CompanyForUpdateModel>;
