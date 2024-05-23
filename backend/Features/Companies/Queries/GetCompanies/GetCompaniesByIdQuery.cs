﻿using API.Common.Models;
using API.Features.Companies.Models;
using MediatR;

namespace API.Features.Companies.Queries.GetCompanies;

public sealed record GetCompaniesByIdQuery(Guid CompanyId) : IRequest<CompanyViewModel>;
