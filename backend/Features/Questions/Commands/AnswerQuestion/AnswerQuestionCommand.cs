using API.Common.Responses;
using MediatR;

namespace API.Features.Questions.Commands.AnswerQuestion
{
    public class AnswerQuestionCommand : IRequest<CreateOrUpdateResponse>
    {
        internal Guid QuestionId { get; set; }
        public string Answer { get; init; }
    }
}
