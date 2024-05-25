using API.Common.Exceptions;

namespace API.Features.Questions.Exceptions;

public sealed class QuestionNotFoundException : BaseException
{
    public QuestionNotFoundException() : base("Pytanie nie istnieje") { }
}