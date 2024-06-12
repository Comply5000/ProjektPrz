using FluentValidation;

namespace API.Features.Questions.Commands.Create
{
    public class CreateQuestionValidator : AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Brak treści pytania");
        }
    }
}
