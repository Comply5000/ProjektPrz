using API.Common.Responses;
using MediatR;

namespace API.Features.Questions.Commands.Create
{
    public class CreateQuestionCommand : IRequest<CreateOrUpdateResponse>
    {
        public string Message { get; init; }
        internal Guid OfferId { get; set; }
    }
}
