using API.Database.Context;
using API.Features.Companies.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using API.Features.Companies.Exceptions;
using API.Features.Identity.Services;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace API.Features.Companies.Queries.GetCompanyById
{
    public class GetCompanyByIdHandler : IRequestHandler<GetCompanyByIdQuery, CompanyViewModel>
    {
        private readonly EFContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetCompanyByIdHandler(EFContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<CompanyViewModel> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _context.Companies
                .Include(x => x.Image)
                .Include(x => x.Followers)
                .FirstOrDefaultAsync(x => x.Id == request.CompanyId, cancellationToken);

            if (company == null)
            {
                // Możesz rzucić wyjątek lub zwrócić null w zależności od Twoich wymagań
                throw new CompanyNotFoundException();
            }
            
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken);

            return new CompanyViewModel
            {
                Id = company.Id,
                Name = company.Name,
                Localization = company.Localization,
                ImageUrl = company.Image?.Url,
                Description = company.Description,
                Favourite = company.Followers.Contains(user) //sprawdzenie czy użytkownik ma firmę w polubionych
            };
        }
    }
}
