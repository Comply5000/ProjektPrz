using API.Database.Context;
using API.Features.Identity.Entities;
using API.Features.Identity.Exeptions;
using API.Features.Identity.Static;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Principal;
using API.Common.MessageBroker.Services;
using API.Features.Identity.Events;
using API.Features.Identity.Events.SendConfirmAccountEmail;
using API.Features.Identity.Exceptions;

namespace API.Features.Identity.Commands.SingUp
{
    public class SignUpCommandHandler : IRequestHandler<SingUpCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly EFContext _context;
        private readonly IEventBus _eventBus;

        // Konstruktor do wstrzykiwania zależności.
        public SignUpCommandHandler(UserManager<User> userManager, EFContext context, IEventBus eventBus)
        {
            _userManager = userManager;
            _context = context;
            _eventBus = eventBus;
        }

        public async Task Handle(SingUpCommand request, CancellationToken cancellationToken)
        {
            // Sprawdza, czy użytkownik o podanym adresie e-mail już istnieje.
            var isUserExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email);
            if (isUserExists)
            {
                // Jeśli tak, rzuca wyjątek.
                throw new UserWithEmailExistsExaptions();
            }

            // Tworzy nowego użytkownika.
            var user = new User
            {
                Email = request.Email,
                UserName = request.Email,
            };

            // Tworzy użytkownika w systemie uwierzytelniania.
            var createdUser = await _userManager.CreateAsync(user, request.Password);

            // Jeśli tworzenie użytkownika nie powiedzie się, rzuca wyjątek.
            if (!createdUser.Succeeded)
            {
                throw new CreateUserException(createdUser.Errors);
            }

            // Dodaje użytkownikowi rolę użytkownika.
            var addRoleResult = await _userManager.AddToRoleAsync(user, UserRoles.User);
            if (!addRoleResult.Succeeded)
            {
                // Jeśli dodanie roli nie powiedzie się, rzuca wyjątek.
                throw new AddUserToRoleExeption();
            }


            // Dodaje użytkownikowi oświadczenie e-mail.
            var addEmailClaimResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
            if (!addEmailClaimResult.Succeeded)
                throw new AddClaimException();

            // Dodaje użytkownikowi oświadczenie identyfikatora.
            var addNameClaimResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            if (!addNameClaimResult.Succeeded)
                throw new AddClaimException();

            var confirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            
            // Zapisuje zmiany w bazie danych.
            await _context.SaveChangesAsync(cancellationToken);

            await _eventBus.PublishAsync(new SendConfirmAccountEmailEvent
            {
                Email = request.Email,
                Token = confirmToken,
                UserId = user.Id
            }, cancellationToken);
        }
    }
}