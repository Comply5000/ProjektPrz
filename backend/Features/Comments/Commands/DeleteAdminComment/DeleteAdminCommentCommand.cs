using MediatR;

namespace API.Features.Comments.Commands.DeleteAdminComment;

public sealed record DeleteAdminCommentCommand(
    Guid Id
    ) : IRequest;


