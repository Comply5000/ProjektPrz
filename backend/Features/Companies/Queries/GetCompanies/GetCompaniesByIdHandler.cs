using API.Database.Context;
using API.Features.Companies.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Features.Companies.Queries.GetCompanies
{
    public class GetCompaniesByIdHandler : IRequestHandler<GetCompaniesByIdQuery, CompanyViewModel>
    {
        private readonly EFContext _context;

        public GetCompaniesByIdHandler(EFContext context)
        {
            _context = context;
        }

        public async Task<CompanyViewModel> Handle(GetCompaniesByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _context.Companies
                .Include(x => x.Image)
                .FirstOrDefaultAsync(x => x.Id == request.CompanyId);

            if (company == null)
            {
                // Możesz rzucić wyjątek lub zwrócić null w zależności od Twoich wymagań
                throw new Exception($"Company with ID {request.CompanyId} not found.");
            }

            return new CompanyViewModel
            {
                Name = company.Name,
                Localization = company.Localization,
                ImageUrl = company.Image?.Url,
                Description = company.Description,
                ImageId = company.ImageId
            };
        }
    }
}
