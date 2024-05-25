using MediatR;
using System;

namespace API.Features.Questions.Commands
{
    public class AnswerQuestionCommand : IRequest<Guid>
    {
        public Guid QuestionId { get; set; }
        public string Answer { get; set; }
        public Guid UserId { get; set; } // kto udzielil odpowiedzi
    }
}
