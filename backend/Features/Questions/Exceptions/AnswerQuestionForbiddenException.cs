using API.Common.Exceptions;

namespace API.Features.Questions.Exceptions;

public sealed class AnswerQuestionForbiddenException : BaseException
{
    public AnswerQuestionForbiddenException() : base("Nie masz uprawnień by odpowiedzieć na to pytanie") { }
}