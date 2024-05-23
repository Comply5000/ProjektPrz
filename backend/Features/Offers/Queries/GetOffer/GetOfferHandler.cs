using API.Common.Extensions;
using API.Common.Models;
using API.Database.Context;
using API.Features.Companies.Models;
using API.Features.Offers.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Offers.Queries.GetOffer
{
    public class GetOfferHandler:IRequestHandler<GetOfferQuery,PaginatedList<OffersListModel>>
    {
        private readonly EFContext _context;

        public GetOfferHandler(EFContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<OffersListModel>> Handle(GetOfferQuery request, CancellationToken cancellationToken)
        {
            //check data
            var query = _context.Offers.AsNoTracking()
                .Where(x => x.DateFrom < DateTime.Today && x.DateTo > DateTime.UtcNow);

            //check by name
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(x => x.Name.Contains(request.Search));
            }

            //check by type
            /*if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(x => x.CompanyId==request.Search);
            }*/


            var offers = await query
                .Include(x => x.Image)
                .Select(x => new OffersListModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.Image.Url,
                })
                .ToPaginatedListAsync(request, cancellationToken);
            return offers;

        }
    }
}
