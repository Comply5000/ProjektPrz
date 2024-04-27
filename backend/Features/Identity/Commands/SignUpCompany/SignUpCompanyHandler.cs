using System.Security.Claims;
using API.Database.Context;
using API.Features.Companies.Entities;
using API.Features.Companies.Exceptions;
using API.Features.Identity.Entities;
using API.Features.Identity.Exeptions;
using API.Features.Identity.Static;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Identity.Commands.SignUpCompany;

public sealed class SignUpCompanyHandler : IRequestHandler<SignUpCompanyCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly EFContext _context;

    public SignUpCompanyHandler(UserManager<User> userManager, EFContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    
    public async Task Handle(SignUpCompanyCommand request, CancellationToken cancellationToken)
    {
        // Sprawdza, czy użytkownik o podanym adresie e-mail już istnieje.
        var isUserExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (isUserExists)
        {
            // Jeśli tak, rzuca wyjątek.
            throw new UserWithEmailExistsExaptions();
        }

        var isCompanyExists = await _context.Companies.AnyAsync(x => EF.Functions.ILike(x.Name, request.CompanyName), cancellationToken);
        if (isCompanyExists)
            throw new CompanyWithNameExistsException();

        // Tworzy nowego użytkownika.
        var user = new User
        {
            Email = request.Email,
            UserName = request.Email,
        };
        
        // Tworzy użytkownika w systemie uwierzytelniania.
        var createdUser = await _userManager.CreateAsync(user, request.Password);
        await _context.SaveChangesAsync(cancellationToken);
        
        var company = new Company()
        {
            Name = request.CompanyName,
            UserId = user.Id
        };

        await _context.AddAsync(company, cancellationToken);

        // Jeśli tworzenie użytkownika nie powiedzie się, rzuca wyjątek.
        if (!createdUser.Succeeded)
        {
            throw new CreateUserException(createdUser.Errors);
        }

        // Dodaje użytkownikowi rolę użytkownika.
        var addUserRoleResult = await _userManager.AddToRoleAsync(user, UserRoles.User);
        if (!addUserRoleResult.Succeeded)
        {
            // Jeśli dodanie roli nie powiedzie się, rzuca wyjątek.
            throw new AddUserToRoleExeption();
        }
        
        var addCompanyRoleResult = await _userManager.AddToRoleAsync(user, UserRoles.CompanyOwner);
        if (!addCompanyRoleResult.Succeeded)
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
        
        // Dodaje użytkownikowi oświadczenie identyfikatora.
        var addCompanyIdClaimResult = await _userManager.AddClaimAsync(user, new Claim("CompanyId", company.Id.ToString()));
        if (!addNameClaimResult.Succeeded)
            throw new AddClaimException();

        // Zapisuje zmiany w bazie danych.
        await _context.SaveChangesAsync(cancellationToken);
    }
}