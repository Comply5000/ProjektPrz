using API.Common.Enums;
using API.Common.Responses;
using API.Database.Context;
using API.Features.Identity.Services;
using API.Features.Offers.Exeptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace API.Features.Offers.Commands.DeleteOffer
{
    public class DeleteOfferHandler : IRequestHandler<DeleteOfferCommand, CreateOrUpdateResponse>
    {
        //tworzyć repository dla ofert?
        private readonly EFContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public DeleteOfferHandler(EFContext context, ICurrentUserService currentUserService, IMediator mediator)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task<CreateOrUpdateResponse> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
        {
            //czy taka oferta isntnieje
            var offer = await _context.Offers.FirstOrDefaultAsync(x => x.Id == request.Id /*&& x.CompanyId == _currentUserService.CompanyId*/, cancellationToken)
                ?? throw new OfferNorFoundExeption();

            offer.EntryStatus = EntryStatus.Deleted;

            var result = _context.Update(offer);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateOrUpdateResponse(result.Entity.Id);
        }
    }
}
