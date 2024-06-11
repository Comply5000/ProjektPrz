using Amazon.Runtime.Internal;
using API.Common.Models;
using API.Features.Questions.Models;
using API.Features.Offers.Enums;
using MediatR;


namespace API.Features.Questions.Queries.GetQuestion
{
    public sealed record GetQuestionsQuery(Guid OfferId) : IRequest<List<QuestionModel>>;

}
