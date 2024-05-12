using API.Common.Responses;
using API.Database.Context;
using API.Features.Companies.Exceptions;
using API.Features.Identity.Services;
using API.Features.Images.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Companies.Commands.Update;

public sealed class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, CreateOrUpdateResponse>
{
    private readonly EFContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMediator _mediator;

    public UpdateCompanyHandler(EFContext context, ICurrentUserService currentUserService, IMediator mediator)
    {
        _context = context;
        _currentUserService = currentUserService;
        _mediator = mediator;
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
        company.ImageId = null;

        if (request.Image is not null)
        {
            var uploadResult = await _mediator.Send(new UploadImageCommand(request.Image), cancellationToken);
            company.ImageId = uploadResult;
        }

        var result = _context.Update(company);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateOrUpdateResponse(result.Entity.Id);
    }
}