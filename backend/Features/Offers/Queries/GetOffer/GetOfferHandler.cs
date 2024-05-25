using API.Common.Extensions;
using API.Common.Models;
using API.Database.Context;
using API.Features.Companies.Models;
using API.Features.Offers.Models;
using MassTransit.SagaStateMachine;
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
                .Where(x => x.DateFrom < DateTime.Today && x.DateTo > DateTime.Today)
                .Where(x => x.EntryStatus == Common.Enums.EntryStatus.Active);

            //check by name
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(x => x.Name.Contains(request.Search));
            }
            //check by type
            if (request.Type != 0)
                query = query.Where(x=>x.Type == request.Type);

            //check by id company
            if (request.CompanyId != Guid.Empty)
                query = query.Where(x => x.CompanyId == request.CompanyId);




            var offers = await query
                .Include(x => x.Image)
                .Select(x => new OffersListModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Type = x.Type,
                    ImageUrl = x.Image,
                    CompanyId = x.CompanyId,
                })
                .ToPaginatedListAsync(request, cancellationToken);
            return offers;

        }
    }
}
