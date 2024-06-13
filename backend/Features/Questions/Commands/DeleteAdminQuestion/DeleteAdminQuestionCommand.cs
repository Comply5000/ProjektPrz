using MediatR;

namespace API.Features.Questions.Commands.DeleteAdminQuestion;

public sealed record DeleteAdminQuestionCommand(Guid Id) : IRequest;


