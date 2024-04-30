using System.Security.Claims;
using API.Database.Context;
using API.Features.Identity.Entities;
using API.Features.Identity.Exceptions;
using API.Features.Identity.Exeptions;
using API.Features.Identity.Static;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Database.Initializer.Admin;

internal static class AdminSeeder
{
    private const string Email = "admin@admin.pl";
    private const string Password = "1qazXSW@";
    
    internal static async Task SeedAsync(UserManager<User> userManager, EFContext context, CancellationToken cancellationToken)
    {
        var isUserExists = await userManager.Users.AnyAsync(x => x.Email == Email, cancellationToken);
        if (isUserExists)
            return;
        
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        var user = new User
        {
            Email = Email,
            UserName = Email,
        };

        // Tworzy użytkownika w systemie uwierzytelniania.
        var createdUser = await userManager.CreateAsync(user, Password);

        // Jeśli tworzenie użytkownika nie powiedzie się, rzuca wyjątek.
        if (!createdUser.Succeeded)
        {
            throw new CreateUserException(createdUser.Errors);
        }

        // Dodaje użytkownikowi rolę użytkownika.
        var addUserRoleResult = await userManager.AddToRoleAsync(user, UserRoles.User);
        if (!addUserRoleResult.Succeeded)
        {
            // Jeśli dodanie roli nie powiedzie się, rzuca wyjątek.
            throw new AddUserToRoleExeption();
        }
        
        // Dodaje użytkownikowi rolę użytkownika.
        var addAdminRoleResult = await userManager.AddToRoleAsync(user, UserRoles.Admin);
        if (!addAdminRoleResult.Succeeded)
        {
            // Jeśli dodanie roli nie powiedzie się, rzuca wyjątek.
            throw new AddUserToRoleExeption();
        }


        // Dodaje użytkownikowi oświadczenie e-mail.
        var addEmailClaimResult = await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
        if (!addEmailClaimResult.Succeeded)
            throw new AddClaimException();

        // Dodaje użytkownikowi oświadczenie identyfikatora.
        var addNameClaimResult = await userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        if (!addNameClaimResult.Succeeded)
            throw new AddClaimException();
            
        // Zapisuje zmiany w bazie danych.
        await context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
    }

}