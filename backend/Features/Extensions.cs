using API.Database.Context;
using API.Database.Initializer;
using API.Features.Identity.Services;
using API.Features.Images.Configuration;
using API.Features.Images.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Features
{
    public static class Extensions
    {
        public static IServiceCollection AddFeatures(this IServiceCollection services, IConfiguration configuration)
        {
            var s3Config = new S3Config();
            configuration.GetSection("S3Service").Bind(s3Config);
            services.AddSingleton(s3Config);

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IS3StorageService, S3StorageService>();

            return services;
        }
    }
}
