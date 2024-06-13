using API.Database.Context;
using API.Features.Identity.Services;
using API.Features.Questions.Commands.DeleteQuestion;
using API.Features.Questions.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Questions.Commands.DeleteAdminQuestion;

public class DeleteAdminQuestionHandler : IRequestHandler<DeleteAdminQuestionCommand>
{
    private readonly EFContext _context;
    private readonly ICurrentUserService _currentUserService;

    public DeleteAdminQuestionHandler(EFContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task Handle(DeleteAdminQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new QuestionNotFoundException();
        
        _context.Remove(question);
        await _context.SaveChangesAsync(cancellationToken);

    }
}
