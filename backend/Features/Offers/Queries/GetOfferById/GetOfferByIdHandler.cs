using API.Common.Models;
using API.Database.Context;
using API.Features.Identity.Services;
using API.Features.Offers.Exeptions;
using API.Features.Offers.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Offers.Queries.GetOfferById
{
    public class GetOfferByIdHandler : IRequestHandler<GetOfferByIdQuery, OfferModel>
    {
        private readonly EFContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetOfferByIdHandler(EFContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<OfferModel> Handle(GetOfferByIdQuery query, CancellationToken cancellationToken)
        {
            var offer = await _context.Offers.AsNoTracking()
                            .Where(x => x.Id == query.Id)
                            .Include(x => x.Image)
                            .Select(x => new OfferModel
                            {
                                Id = x.Id,
                                Name = x.Name,
                                Description = x.Description,
                                Type = x.Type,
                                ImageUrl = x.Image.Url,
                                CompanyId = x.CompanyId,
                                CompanyName = x.Company.Name,
                                IsUserCommented = x.Comments.Any(y => y.UserId == _currentUserService.UserId)
                            })
                            .FirstOrDefaultAsync(cancellationToken)
                        ?? throw new OfferNorFoundExeption();

            return offer;
        }
    }
    
    
    
    
}
