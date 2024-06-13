using API.Database.Context;
using API.Features.Questions.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace API.Features.Questions.Queries.GetQuestion
{
    public class GetQuestionsHandler : IRequestHandler<GetQuestionsQuery, List<QuestionModel>>
    {
        private readonly EFContext _context;

        public GetQuestionsHandler(EFContext context)
        {
            _context = context;
        }

        public async Task<List<QuestionModel>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Questions.AsNoTracking()
                .Where(x => x.OfferId == request.OfferId)
                .Include(x => x.User)
                .Select(x => new QuestionModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    CreatedAt = x.CreatedAt,
                    AnsweredAt = x.AnsweredAt,
                    Answer = x.Answer,
                    CreatedBy = x.User.Email,
                    CreatedById = x.UserId
                })
                .ToListAsync(cancellationToken);
        }
    }
}
