using API.Features.Comments.Models;
using MediatR;
using Newtonsoft.Json.Linq;

namespace API.Features.Comments.Queries.GetComment
{
    public class GetCommentHandler : IRequestHandler<GetCommentQuery, List<CommentModel>>
    {
        public Task<List<CommentModel>> Handle(GetCommentQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
