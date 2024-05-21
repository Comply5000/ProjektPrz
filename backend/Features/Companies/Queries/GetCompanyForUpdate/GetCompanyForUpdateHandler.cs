using API.Database.Context;
using API.Features.Companies.Models;
using API.Features.Identity.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Companies.Queries.GetCompanyForUpdate;

public sealed class GetCompanyForUpdateHandler : IRequestHandler<GetCompanyForUpdateQuery, CompanyForUpdateModel>
{
    private readonly EFContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetCompanyForUpdateHandler(EFContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    
    public async Task<CompanyForUpdateModel> Handle(GetCompanyForUpdateQuery request, CancellationToken cancellationToken)
    {
        return await _context.Companies.AsNoTracking()
            .Include(x => x.Image)
            .Where(x => x.Id == _currentUserService.CompanyId)
            .Select(x => new CompanyForUpdateModel
            {
                Name = x.Name,
                Description = x.Description,
                Localization = x.Localization,
                ImageUrl = x.Image.Url
            })
            .FirstOrDefaultAsync(cancellationToken);


    }
}