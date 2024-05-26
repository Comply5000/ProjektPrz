using API.Common.Extensions;
using API.Common.Models;
using API.Database.Context;
using API.Features.Companies.Models;
using API.Features.Identity.Services;
using API.Features.Offers.Models;
using MassTransit.SagaStateMachine;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Offers.Queries.GetOffer
{
    public class GetOffersHandler:IRequestHandler<GetOffersQuery,PaginatedList<OffersListModel>>
    {
        private readonly EFContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetOffersHandler(EFContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<PaginatedList<OffersListModel>> Handle(GetOffersQuery request, CancellationToken cancellationToken)
        {
            //check data
            var query = _context.Offers.AsNoTracking()
                .Where(x => x.DateFrom < DateTimeOffset.UtcNow && x.DateTo > DateTimeOffset.UtcNow);

            //check by name
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(x => x.Name.Contains(request.Search));
            }
            //check by type
            if (request.Type is not null)
                query = query.Where(x=>x.Type == request.Type);

            //check by id company
            if (request.CompanyId is not null)
                query = query.Where(x => x.CompanyId == request.CompanyId);


            if (request.isCompanyFavourite && _currentUserService.UserId != Guid.Empty)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId,
                    cancellationToken);
                
                var favouriteCompanies = await _context.Companies
                    .Where(x => x.Followers.Contains(user))
                    .Select(x => x.Id).ToListAsync(cancellationToken);

                query = query.Where(x => favouriteCompanies.Contains(x.CompanyId));
            }
                
            
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
                    CompanyName = x.Company.Name
                })
                .ToPaginatedListAsync(request, cancellationToken);
            return offers;

        }
    }
}
