using API.Common.Models;
using API.Database.Context;
using API.Features.Identity.Services;
using API.Features.Offers.Exeptions;
using API.Features.Offers.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Offers.Queries.GetOfferById
{
    public class GetOfferByIdHandler : IRequestHandler<GetOfferByIdQuery, OffersListModel>
    {
        private readonly EFContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetOfferByIdHandler(EFContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<OffersListModel> Handle(GetOfferByIdQuery query, CancellationToken cancellationToken)
        {
            //check data
            /*var query = _context.Offers.AsNoTracking()
                .Where(x => x.Id==);*/

            return await _context.Offers.AsNoTracking()
                .Include(x => x.Image)
                .Select(x => new OffersListModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Type = x.Type,
                    ImageUrl = x.Image.Url,
                    CompanyId = x.CompanyId,
                    CompanyName = x.Company.Name,
                })
                .FirstOrDefaultAsync(cancellationToken);  
        }
    }
    
    
    
    
}
