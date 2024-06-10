using API.Database.Context;
using API.Features.Companies.Models;
using API.Features.Identity.Services;
using API.Features.Offers.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace API.Features.Offers.Queries.GetOfferForUpdate;

public sealed class GetOfferForUpdateHandler : IRequestHandler<GetOfferForUpdateQuery, OfferUpdateModel>
{
    private readonly EFContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetOfferForUpdateHandler(EFContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    
    public async Task<OfferUpdateModel> Handle(GetOfferForUpdateQuery request, CancellationToken cancellationToken)
    {
        return await _context.Offers.AsNoTracking()
            .Include(x => x.Image)
            .Where(x => x.CompanyId == _currentUserService.CompanyId && x.Id == request.Id)
            .Select(x => new OfferUpdateModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                DateFrom = x.DateFrom,
                DateTo = x.DateTo,
                Type = x.Type,
                ImageUrl = x.Image.Url
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}