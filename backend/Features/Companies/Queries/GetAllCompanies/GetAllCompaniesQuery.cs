using API.Common.Models;
using MediatR;

namespace API.Features.Companies.Queries.GetAllCompanies;

public sealed record GetAllCompaniesQuery : IRequest<List<SelectModel>>;
