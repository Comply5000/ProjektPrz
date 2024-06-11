using API.Common.Extensions;
using API.Common.Models;
using API.Database.Context;
using API.Features.Identity.Services;
using API.Features.Offers.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace API.Features.Offers.Queries.GetCompanyOffersList;

public sealed class GetCompanyOffersListHandler : IRequestHandler<GetCompanyOffersListQuery, PaginatedList<OffersListModel>>
{
    private readonly EFContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetCompanyOffersListHandler(EFContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    
    public async Task<PaginatedList<OffersListModel>> Handle(GetCompanyOffersListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Offers.AsNoTracking()
            .Where(x => x.CompanyId == _currentUserService.CompanyId);
        
        //check by name
        if (!string.IsNullOrEmpty(request.Search))
        {
            query = query.Where(x => x.Name.Contains(request.Search));
        }
        //check by type
        if (request.Type is not null)
            query = query.Where(x=>x.Type == request.Type);
        
        var offers = await query
            .Include(x => x.Image)
            .Include(x => x.Company)
            .Select(x => new OffersListModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Type = x.Type,
                ImageUrl = x.Image.Url,
                CompanyId = x.CompanyId,
                CompanyName = x.Company.Name,
                Rating = x.Comments == null || !x.Comments.Any() ? 0 : Math.Round(x.Comments.Select(r => r.Rating).Average(), 1),
            })
            .ToPaginatedListAsync(request, cancellationToken);
        
        return offers;
    }
}