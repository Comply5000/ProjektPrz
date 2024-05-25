using API.Common.Responses;
using MediatR;

namespace API.Features.Comments.Commands.DeleteComment;

public sealed record DeleteCommentCommand(
    Guid Id
    ) : IRequest;


