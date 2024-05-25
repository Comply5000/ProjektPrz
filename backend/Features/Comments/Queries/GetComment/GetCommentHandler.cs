using API.Database.Context;
using API.Features.Comments.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace API.Features.Comments.Queries.GetComment
{
    public class GetCommentHandler : IRequestHandler<GetCommentQuery, List<CommentModel>>
    {
        private readonly EFContext _context;

        public GetCommentHandler(EFContext context)
        {
            _context = context;
        }

        public async Task<List<CommentModel>> Handle(GetCommentQuery request, CancellationToken cancellationToken)
        {
            return await _context.Comments.AsNoTracking()
                .Where(x => x.OfferId == request.OfferId)
                .Select(x => new CommentModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    Rating = x.Rating,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}
