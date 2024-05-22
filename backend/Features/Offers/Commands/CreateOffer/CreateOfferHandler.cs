using Amazon.Runtime.Internal;
using API.Common.Responses;
using API.Database.Context;
using API.Features.Companies.Entities;
using API.Features.Identity.Entities;
using API.Features.Identity.Services;
using API.Features.Images.Commands;
using API.Features.Offers.Entities;
using API.Features.Offers.Exeptions;
using MediatR;

namespace API.Features.Offers.Commands.CreateOffer
{
    /*   public class CreateOfferHandler:IRequestHandler<CreateOfferCommand,CreateOrUpdateResponse>
       {
           private readonly EFContext _context;
           private readonly ICurrentUserService _currentUserService;
           private readonly IMediator _mediator;
           private readonly ILogger _logger;

           public CreateOfferHandler(EFContext context, ICurrentUserService currentUserService, IMediator mediator)
           {
               _context = context;
               _currentUserService = currentUserService;
               _mediator = mediator;
           }

           public async Task<CreateOrUpdateResponse> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
           {
               //TODO
               //ADD czy oferta taka już istnieje ??

               var offer = new Offer
               {
                   Name = request.Name,
                   Description = request.Description,
                   DateFrom = request.DateFrom,
                   DateTo = request.DateTo,
                   ImageId =null,
               //    CompanyId = Companies.Id,
                   Type=request.Type,
               };


               var result = _context.Offers.Add(offer);

               await _context.SaveChangesAsync(cancellationToken);

               return new CreateOrUpdateResponse(result.Entity.Id);

           }
       }
   */
    public class CreateOfferHandler : IRequestHandler<CreateOfferCommand, CreateOrUpdateResponse>
    {
        private readonly EFContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;


        public CreateOfferHandler(EFContext context, ICurrentUserService currentUserService, IMediator mediator, ILogger<CreateOfferHandler> logger)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mediator = mediator;

        }

        public async Task<CreateOrUpdateResponse> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
          
            // Create a new offer
            var offer = new Offer
            {
                Name = request.Name,
                Description = request.Description,
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                ImageId = null,
                CompanyId = _currentUserService.CompanyId,
                Type = request.Type,

            };


            if (request.Image is not null)
            {
                var uploadResult = await _mediator.Send(new UploadImageCommand(request.Image), cancellationToken);
                offer.ImageId = uploadResult;
            }

            // Add offer to the context
            var result = await _context.Offers.AddAsync(offer);

            await _context.SaveChangesAsync(cancellationToken);
            return new CreateOrUpdateResponse(result.Entity.Id);
           
        }
    }

}

