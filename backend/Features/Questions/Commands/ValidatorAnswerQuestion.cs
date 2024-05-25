using FluentValidation;
using API.Features.Questions.Commands;

namespace API.Features.Questions.Validators
{
    public class AnswerQuestionValidator : AbstractValidator<AnswerQuestionCommand>
    {
        public AnswerQuestionValidator()
        {
            RuleFor(x => x.QuestionId).NotEmpty().WithMessage("QuestionId is required");
            RuleFor(x => x.Answer).NotEmpty().WithMessage("Answer is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
        }
    }
}
