using API.Common.Extensions;
using API.Common.Models;
using API.Database.Context;
using API.Features.Companies.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Companies.Queries.GetCompanies;

public sealed class GetCompaniesHandler : IRequestHandler<GetCompaniesQuery, PaginatedList<CompanyListModel>>
{
    private readonly EFContext _context;

    public GetCompaniesHandler(EFContext context)
    {
        _context = context;
    }
    
    public async Task<PaginatedList<CompanyListModel>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Companies.AsNoTracking();
        
        if(!string.IsNullOrWhiteSpace(request.Search))
            query = query.Where(v =>
                EF.Functions.ILike(v.Name, $"%{request.Search}%") ||
                EF.Functions.ILike(v.Localization, $"%{request.Search}%"));

        var companies = await query
            .Include(x => x.Image)
            .Select(x => new CompanyListModel
            {
                Name = x.Name,
                Localization = x.Localization,
                ImageUrl = x.Image.Url
            })
            .ToPaginatedListAsync(request, cancellationToken);

        return companies;
    }
}