using MediatR;
using System;

namespace API.Features.Questions.Commands
{
    public class CreateQuestionCommand : IRequest<Guid>
    {
        public string Message { get; set; }
        public Guid UserId { get; set; }
        public Guid OfferId { get; set; }
    }
}
