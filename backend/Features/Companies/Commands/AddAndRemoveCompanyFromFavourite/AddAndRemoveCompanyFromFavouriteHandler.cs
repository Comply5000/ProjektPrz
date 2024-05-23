using Amazon.Runtime.Internal;
using API.Database.Context;
using API.Features.Companies.Exceptions;
using API.Features.Identity.Exceptions;
using API.Features.Identity.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Companies.Commands.AddCompanyToFavourite;

    public sealed class AddAndRemoveCompanyFromFavouriteHandler : IRequestHandler<AddAndRemoveCompanyFromFavouriteCommand>
    {
        private readonly EFContext _context;
        private readonly ICurrentUserService _currentUserService;
    //konstruktor-przekazywane są param EFContext (kontekst bazy danych) oraz ICurrentUserService (obsługa info o bieżącym uzytkowniku)
    public AddAndRemoveCompanyFromFavouriteHandler(EFContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }
        public async Task Handle(AddAndRemoveCompanyFromFavouriteCommand request, CancellationToken cancellationToken)
    {// // metoda obsluguje dwa obiekty, token do obslugi anulowania operacji
        var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                      ?? throw new CompanyNotFoundException();
        // proba znalezienia firmy przy uzyciu _context, jak nie znajdzie firmy to zwraca wyjatek
            var user = await _context.Users
                            .Include(x=>x.FavouriteCompanies)
                            .FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken)//pobierany jest obiekt użytkownika na podstawie identyfikatora bieżącego użytkownika
                ?? throw new UserNotFoundExeption();

            var isCompanyInFavourite = user.FavouriteCompanies.Any(x => x.Id == company.Id);//sprawdzenie czy firma jest w ulubionych

            if(isCompanyInFavourite)
            {
                user.FavouriteCompanies.Remove(company);
            }
            else
            {
                user.FavouriteCompanies.Add(company);
            }
            _context.Update(user);//aktualizacja uzytkownika w bazie
            await _context.SaveChangesAsync(cancellationToken);
            
        
        }
    }
