using API.Database.Context;
using API.Database.Initializer;
using API.Features.Identity.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Features
{
    public static class Extensions
    {
        public static IServiceCollection AddFeatures(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
