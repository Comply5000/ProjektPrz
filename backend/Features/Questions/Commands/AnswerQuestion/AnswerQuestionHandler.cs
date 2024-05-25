using API.Common.Responses;
using API.Database.Context;
using API.Features.Identity.Services;
using API.Features.Questions.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Questions.Commands.AnswerQuestion
{
    public class AnswerQuestionHandler : IRequestHandler<AnswerQuestionCommand, CreateOrUpdateResponse>
    {
        private readonly EFContext _context;
        private readonly ICurrentUserService _currentUserService;

        public AnswerQuestionHandler(EFContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<CreateOrUpdateResponse> Handle(AnswerQuestionCommand request, CancellationToken cancellationToken)
        {
            var currentCompanyId = _currentUserService.CompanyId;

            var question = await _context.Questions
                .Include(x => x.Offer)
                .FirstOrDefaultAsync(x => x.Id == request.QuestionId, cancellationToken);

            if (question == null)
            {
                throw new QuestionNotFoundException();
            }

            if (question.Offer.CompanyId != currentCompanyId)
            {
                throw new AnswerQuestionForbiddenException();
            }

            question.Answer = request.Answer;
            question.AnsweredAt = DateTimeOffset.UtcNow;

            _context.Questions.Update(question);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateOrUpdateResponse(question.Id);
        }
    }
}
