using Amazon.Runtime.Internal;
using API.Common.Models;
using API.Features.Comments.Models;
using API.Features.Offers.Enums;
using MediatR;


namespace API.Features.Comments.Queries.GetComment
{
    public sealed record GetCommentQuery(Guid OfferId) : IRequest<List<CommentModel>>;
    
}
