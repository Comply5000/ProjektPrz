﻿using API.Common.Enums;
using API.Common.Responses;
using API.Database.Context;
using API.Features.Identity.Services;
using Microsoft.EntityFrameworkCore;
using API.Features.Questions.Exceptions;
using System.Threading;
using API.Common.Exceptions;
using MediatR;

namespace API.Features.Questions.Commands.DeleteQuestion;

public class DeleteQuestionHandler : IRequestHandler<DeleteQuestionCommand>
{
    private readonly EFContext _context;
    private readonly ICurrentUserService _currentUserService;

    public DeleteQuestionHandler(EFContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new QuestionNotFoundException();


        if (question.UserId != _currentUserService.UserId)
        {
            throw new ForbiddenException();
        }

        _context.Remove(question);
        await _context.SaveChangesAsync(cancellationToken);

    }
}
