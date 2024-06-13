using API.Common.Exceptions;
using API.Database.Context;
using API.Features.Comments.Commands.DeleteComment;
using API.Features.Comments.Exeptions;
using API.Features.Identity.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Comments.Commands.DeleteAdminComment;

public class DeleteAdminCommentHandler : IRequestHandler<DeleteAdminCommentCommand>
{
    private readonly EFContext _context;

    public DeleteAdminCommentHandler(EFContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteAdminCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new CommentNotFoundExeption();

        _context.Remove(comment);
        await _context.SaveChangesAsync(cancellationToken);

    }
}
