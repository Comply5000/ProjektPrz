using API.Common.Responses;
using API.Features.Comments.Entities;
using MassTransit.Futures.Contracts;
using MediatR;

namespace API.Features.Comments.Commands.CreateComment;

public sealed record CreateCommentCommand(
    string Message,
    int Rating
    ) : IRequest<CreateOrUpdateResponse>
{
    internal Guid OfferId {  get; set; }    
}
