using API.Common.Extensions;
using API.Common.Models;
using API.Database.Context;
using API.Features.Companies.Models;
using API.Features.Identity.Entities;
using API.Features.Identity.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Companies.Queries.GetCompanies;

public sealed class GetCompaniesHandler : IRequestHandler<GetCompaniesQuery, PaginatedList<CompanyListModel>>
{
    private readonly EFContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetCompaniesHandler(EFContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    
    public async Task<PaginatedList<CompanyListModel>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Companies.AsNoTracking();
        
        if(!string.IsNullOrWhiteSpace(request.Search))
            query = query.Where(v =>
                EF.Functions.ILike(v.Name, $"%{request.Search}%") ||
                EF.Functions.ILike(v.Localization, $"%{request.Search}%"));

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken);

        var companies = await query
            .Include(x => x.Image)
            .Select(x => new CompanyListModel
            {
                Name = x.Name,
                Localization = x.Localization,
                Description = x.Description.Substring(0, 150),
                ImageUrl = x.Image.Url,
                Favourite = x.Followers.Contains(user) //sprawdzenie czy użytkownik ma firmę w polubionych
            })
            .ToPaginatedListAsync(request, cancellationToken);

        return companies;
    }
}