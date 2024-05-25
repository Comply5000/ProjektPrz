using API.Common.Responses;
using API.Database.Context;
using API.Features.Identity.Services;
using API.Features.Questions.Commands;
using API.Features.Questions.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Features.Questions.Handlers
{
    public class AnswerQuestionHandler : IRequestHandler<AnswerQuestionCommand, Guid>
    {
        private readonly EFContext _context;
        private readonly ICurrentUserService _currentUserService;

        public AnswerQuestionHandler(EFContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(AnswerQuestionCommand request, CancellationToken cancellationToken)
        {
            // Pobierz identyfikator bieżącego użytkownika lub firmy
            var currentUserId = _currentUserService.UserId;
            var currentCompanyId = _currentUserService.CompanyId;

            // Wyszukaj pytanie na podstawie jego identyfikatora
            var question = await _context.Questions.FindAsync(request.QuestionId);

            if (question == null)
            {
                throw new ArgumentException("Question not found");
            }

            // Sprawdź, czy bieżący użytkownik lub firma jest właścicielem pytania
            if (question.UserId != currentUserId && question.Offer.CompanyId != currentCompanyId)
            {
                throw new UnauthorizedAccessException("You are not authorized to answer this question");
            }

            // Dodaj odpowiedź do pytania
            question.Answer = request.Answer;
            question.AnsweredAt = DateTimeOffset.UtcNow;

            // Zapisz zmiany w bazie danych
            await _context.SaveChangesAsync(cancellationToken);

            return question.Id;
        }
    }
}
