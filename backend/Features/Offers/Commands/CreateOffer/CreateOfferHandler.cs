﻿using Amazon.Runtime.Internal;
using API.Common.Responses;
using API.Database.Context;
using API.Features.Identity.Services;
using API.Features.Offers.Entities;
using MediatR;

namespace API.Features.Offers.Commands.CreateOffer
{
    public class CreateOfferHandler:IRequestHandler<CreateOfferCommand,CreateOrUpdateResponse>
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
            try
            {
                var offer = new Offer
                {
                    Name = request.Name,
                    Description = request.Description,
                    DateFrom = request.DateFrom,
                    DateTo = request.DateTo,
                    ImageId =null,
                    //CompanyId = 
                };

                var result = _context.Offers.Add(offer);

                await _context.SaveChangesAsync(cancellationToken);

                return new CreateOrUpdateResponse(result.Entity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the entity changes. Inner exception: {InnerException}", ex.InnerException?.Message);
                throw;
            }
        }
    }
}
