using API.Database.Context;
using API.Features.Identity.Static;
using Microsoft.AspNetCore.Identity;

namespace API.Database.Initializer.Roles
{
    internal static class UserRolesSeeder
    {
        internal static async Task SeedAsync(RoleManager<IdentityRole<Guid>> roleManager, EFContext context, CancellationToken cancellationToken)
        {
            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            var roles = UserRoles.Get();
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }

            await transaction.CommitAsync(cancellationToken);
        }

    }
}
