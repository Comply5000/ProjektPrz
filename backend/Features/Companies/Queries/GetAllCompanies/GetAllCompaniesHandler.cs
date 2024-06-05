using API.Common.Models;
using API.Database.Context;
using API.Features.Companies.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Companies.Queries.GetAllCompanies;

public sealed class GetAllCompaniesHandler : IRequestHandler<GetAllCompaniesQuery, List<SelectModel>>
{
    private readonly EFContext _context;

    public GetAllCompaniesHandler(EFContext context)
    {
        _context = context;
    }
    
    public async Task<List<SelectModel>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Companies.AsNoTracking()
            .Where(x => x.User.EmailConfirmed)
            .Select(x => new Company
            {
                Id = x.Id,
                Name = x.Name
            })
            .Select(x => new SelectModel
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync(cancellationToken);
    }
}