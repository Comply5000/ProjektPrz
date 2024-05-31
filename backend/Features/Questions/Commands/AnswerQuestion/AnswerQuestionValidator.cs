using FluentValidation;

namespace API.Features.Questions.Commands.AnswerQuestion
{
    public class AnswerQuestionValidator : AbstractValidator<AnswerQuestionCommand>
    {
        public AnswerQuestionValidator()
        {
            RuleFor(x => x.Answer).NotEmpty().WithMessage("Answer is required");
        }
    }
}
