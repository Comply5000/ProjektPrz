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
    public class DeleteOfferHandler : IRequestHandler<DeleteOfferCommand>
    {
        private readonly EFContext _context;
        private readonly ICurrentUserService _currentUserService;

        public DeleteOfferHandler(EFContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
        {
            //czy taka oferta isntnieje
            var offer = await _context.Offers.FirstOrDefaultAsync(x => x.Id == request.Id && x.CompanyId == _currentUserService.CompanyId, cancellationToken)
                ?? throw new OfferNorFoundExeption();

            _context.Remove(offer);
            await _context.SaveChangesAsync(cancellationToken); ;
        }
    }
}
