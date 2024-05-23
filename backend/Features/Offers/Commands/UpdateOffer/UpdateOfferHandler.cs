using API.Common.Responses;
using API.Database.Context;
using API.Features.Identity.Services;
using API.Features.Images.Commands;
using API.Features.Offers.Exeptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Offers.Commands.UpdateOffer
{
    public class UpdateOfferHandler:IRequestHandler<UpdateOfferCommand,CreateOrUpdateResponse>
    {
        private readonly EFContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public UpdateOfferHandler(EFContext context, ICurrentUserService currentUserService, IMediator mediator)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task<CreateOrUpdateResponse> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
        {
            //czy taka oferta isntnieje
            var offer = await _context.Offers.FirstOrDefaultAsync(x => x.Id == request.Id && x.CompanyId==_currentUserService.CompanyId, cancellationToken)
                ?? throw new OfferNorFoundExeption();
            

            //update danych
            offer.Name = request.Name;
            offer.Description = request.Description;
            offer.Image = null;
            offer.DateTo = request.DateTo;

            if(request.Image != null)
            {
                var uploadResult = await _mediator.Send(new UploadImageCommand(request.Image),cancellationToken);
                offer.ImageId = uploadResult;
            }

            var result = _context.Update(offer);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateOrUpdateResponse(result.Entity.Id);
        }
    }
}
