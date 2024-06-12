using API.Common.Responses;
using API.Database.Context;
using API.Features.Identity.Services;
using API.Features.Images.Commands;
using Amazon.Runtime.Internal;
using API.Common.Exceptions;
using API.Features.Companies.Entities;
using API.Features.Identity.Entities;
using API.Features.Comments.Commands.CreateComment;
using API.Features.Comments.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Comments.Commands.CreateComment
{
    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, CreateOrUpdateResponse>
    {
        private readonly EFContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public CreateCommentHandler(EFContext context, ICurrentUserService currentUserService, IMediator mediator, ILogger<CreateCommentHandler> logger)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mediator = mediator;

        }

        public async Task<CreateOrUpdateResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.AsNoTracking()
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken);

            if (user.Comments.Any(x => x.OfferId == request.OfferId))
                throw new ForbiddenException();
            
            // Create a new Comment
            var comment = new Comment
            {
                Message = request.Message,
                Rating = request.Rating,
                CreatedAt = DateTimeOffset.UtcNow,
                UserId = _currentUserService.UserId,
                OfferId = request.OfferId
            };


            // Add comment to the context
            var result = await _context.Comments.AddAsync(comment);

            await _context.SaveChangesAsync(cancellationToken);
            return new CreateOrUpdateResponse(result.Entity.Id);

        }
    }
}

