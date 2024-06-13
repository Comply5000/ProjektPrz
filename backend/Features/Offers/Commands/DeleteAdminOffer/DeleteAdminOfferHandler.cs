using API.Database.Context;
using API.Features.Identity.Services;
using API.Features.Offers.Exeptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Offers.Commands.DeleteAdminOffer
{
    public class DeleteAdminOfferCommandHandler : IRequestHandler<DeleteAdminOfferCommand>
    {
        private readonly EFContext _context;
        private readonly ICurrentUserService _currentUserService;

        public DeleteAdminOfferCommandHandler(EFContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task Handle(DeleteAdminOfferCommand request, CancellationToken cancellationToken)
        {
            //czy taka oferta isntnieje
            var offer = await _context.Offers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                ?? throw new OfferNorFoundExeption();

            _context.Remove(offer);
            await _context.SaveChangesAsync(cancellationToken); ;
        }
    }
}
