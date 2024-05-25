using API.Database.Context;
using API.Features.Identity.Services;
using API.Features.Questions.Commands;
using API.Features.Questions.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Features.Questions.Handlers
{
    public class AnswerQuestionHandler : IRequestHandler<AnswerQuestionCommand, Guid>
    {
        private readonly EFContext _context;
        private readonly ICurrentUserService _currentUserService;

        public AnswerQuestionHandler(EFContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(AnswerQuestionCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId;
            var currentCompanyId = _currentUserService.CompanyId;

            var question = await _context.Questions.FindAsync(request.QuestionId);

            if (question == null)
            {
                throw new ArgumentException("Question not found");
            }

            if (question.UserId != currentUserId && question.Offer.CompanyId != currentCompanyId)
            {
                throw new UnauthorizedAccessException("You are not authorized to answer this question");
            }

            question.Answer = request.Answer;
            question.AnsweredAt = DateTimeOffset.UtcNow;

            _context.Questions.Update(question);
            await _context.SaveChangesAsync(cancellationToken);

            return question.Id;
        }
    }
}
