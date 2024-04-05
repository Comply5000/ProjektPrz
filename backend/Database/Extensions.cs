using API.Database.Context;
using API.Database.Initializer;
using Microsoft.EntityFrameworkCore;

namespace API.Database;

public static class Extensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EFContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DatabaseConnection")!,
                opt =>
                {
                    opt.CommandTimeout(30);
                    opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }).LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
        });
        
        services.AddHostedService<DatabaseInitializer>();

        return services;
    }
}