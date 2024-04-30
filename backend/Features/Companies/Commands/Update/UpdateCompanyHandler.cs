using API.Common.Responses;
using API.Database.Context;
using API.Features.Companies.Exceptions;
using API.Features.Identity.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Companies.Commands.Update;

public sealed class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, CreateOrUpdateResponse>
{
    private readonly EFContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateCompanyHandler(EFContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    
    public async Task<CreateOrUpdateResponse> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == _currentUserService.CompanyId, cancellationToken)
                      ?? throw new CompanyNotFoundException();

        var isCompanyExists = await _context.Companies.AnyAsync(x => EF.Functions.ILike(x.Name, request.Name) 
            && x.Id != _currentUserService.CompanyId, cancellationToken);
        if (isCompanyExists)
            throw new CompanyWithNameExistsException();
        
        company.Name = request.Name;
        company.Description = request.Description;
        company.Localization = request.Localization;

        var result = _context.Update(company);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateOrUpdateResponse(result.Entity.Id);
    }
}