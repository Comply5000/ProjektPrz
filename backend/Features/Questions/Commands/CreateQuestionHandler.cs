using API.Database.Context;
using API.Features.Questions.Entities;
using API.Features.Questions.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Features.Questions.Handlers
{
    public class CreateQuestionHandler : IRequestHandler<CreateQuestionCommand, Guid>
    {
        private readonly EFContext _context;

        public CreateQuestionHandler(EFContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = new Question
            {
                Message = request.Message,
                UserId = request.UserId,
                OfferId = request.OfferId,
                CreatedAt = DateTimeOffset.UtcNow
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync(cancellationToken);

            return question.Id;
        }
    }
}
