using FluentValidation;

namespace API.Features.Questions.Commands.AnswerQuestion
{
    public class AnswerQuestionValidator : AbstractValidator<AnswerQuestionCommand>
    {
        public AnswerQuestionValidator()
        {
            RuleFor(x => x.QuestionId).NotEmpty().WithMessage("QuestionId is required");
            RuleFor(x => x.Answer).NotEmpty().WithMessage("Answer is required");
        }
    }
}
