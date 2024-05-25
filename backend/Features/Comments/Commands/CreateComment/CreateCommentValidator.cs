using FluentValidation;

namespace API.Features.Comments.Commands.CreateComment;

public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentValidator()
    {
        RuleFor(x => x.Rating).NotEmpty().WithMessage("Rating required");
    }
}