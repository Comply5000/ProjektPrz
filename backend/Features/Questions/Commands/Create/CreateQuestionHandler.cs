using API.Common.Responses;
using API.Database.Context;
using API.Features.Identity.Services;
using API.Features.Questions.Entities;
using MediatR;

namespace API.Features.Questions.Commands.Create
{
    public class CreateQuestionHandler : IRequestHandler<CreateQuestionCommand, CreateOrUpdateResponse>
    {
        private readonly EFContext _context;
        private readonly ICurrentUserService _currentUserService;

        public CreateQuestionHandler(EFContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<CreateOrUpdateResponse> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = new Question
            {
                Message = request.Message,
                UserId = _currentUserService.UserId,
                OfferId = request.OfferId,
                CreatedAt = DateTimeOffset.UtcNow
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateOrUpdateResponse(question.Id);
        }
    }
}
