using FluentValidation;

namespace API.Features.Comments.Commands.CreateComment;

public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentValidator()
    {
        RuleFor(x => x.Message).NotEmpty().WithMessage("Treść opinii jest wymagana");
        RuleFor(x => x.Rating).NotEmpty().WithMessage("Ocena jest wymagana");
        RuleFor(x => x.Rating).InclusiveBetween(1, 5).WithMessage("Ocena musi być z zakresu 1-5");
    }
}