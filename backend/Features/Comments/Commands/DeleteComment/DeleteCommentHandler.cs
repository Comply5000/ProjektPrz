using API.Common.Enums;
using API.Common.Responses;
using API.Database.Context;
using API.Features.Identity.Services;
using Microsoft.EntityFrameworkCore;
using API.Features.Comments.Exeptions;
using System.Threading;
using MediatR;

namespace API.Features.Comments.Commands.DeleteComment;

public class DeleteCommentHandler : IRequestHandler<DeleteCommentCommand>
{
    private readonly EFContext _context;
    private readonly ICurrentUserService _currentUserService;

    public DeleteCommentHandler(EFContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new CommentNotFoundExeption();

        _context.Remove(comment);
        await _context.SaveChangesAsync(cancellationToken);

    }
}
