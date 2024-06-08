using API.Common.Responses;
using MediatR;

namespace API.Features.Questions.Commands.DeleteQuestion;

public sealed record DeleteQuestionCommand(Guid Id) : IRequest;


