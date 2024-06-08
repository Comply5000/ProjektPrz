using API.Database.Context;
using API.Features.Questions.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace API.Features.Questions.Queries.GetQuestion
{
    public class GetQuestionHandler : IRequestHandler<GetQuestionQuery, List<QuestionModel>>
    {
        private readonly EFContext _context;

        public GetQuestionHandler(EFContext context)
        {
            _context = context;
        }

        public async Task<List<QuestionModel>> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
        {
            return await _context.Questions.AsNoTracking()
                .Where(x => x.OfferId == request.OfferId)
                .Select(x => new QuestionModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}
