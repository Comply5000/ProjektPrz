using API.Common.Models;using API.Features.Companies.Models;
using MediatR;

namespace API.Features.Companies.Queries.GetCompanies;

public sealed record GetCompaniesQuery(string Search) : PaginationRequest, IRequest<PaginatedList<CompanyListModel>>;
