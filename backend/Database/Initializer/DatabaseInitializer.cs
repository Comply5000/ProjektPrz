using API.Database.Context;
using API.Database.Initializer.Admin;
using API.Database.Initializer.Roles;
using API.Features.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Database.Initializer;

public sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetService(typeof(EFContext)) as EFContext;

        var roleManager = scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole<Guid>>)) as RoleManager<IdentityRole<Guid>>;
        var userManager = scope.ServiceProvider.GetService(typeof(UserManager<User>)) as UserManager<User>;

        if (context is not null)
        {
            await context.Database.MigrateAsync(cancellationToken);

            await UserRolesSeeder.SeedAsync(roleManager, context, cancellationToken);
            await AdminSeeder.SeedAsync(userManager, context, cancellationToken);
        }

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}