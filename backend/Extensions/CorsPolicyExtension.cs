using API.Shared.Configurations.Cors;//Import the namespace containing the CorsConfig class

namespace API.API.Extensions;

public static class CorsPolicyExtension //Extension for IServiceCollection to add CORS configuration
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration) //IServiceCollection extension method for adding CORS configuration
    {
        var corsOptions = new CorsConfig(); //Creating a CorsConfig object and loading settings from the configuration
        configuration.GetSection("Cors").Bind(corsOptions);

        return services
            .AddSingleton(corsOptions)//Adding CorsConfig as a Singleton so it can be used elsewhere
            .AddCors(cors =>
            {
                //Get CORS settings from CorsConfig
                var allowedHeaders = corsOptions.AllowedHeaders;
                var allowedMethods = corsOptions.AllowedMethods;
                var allowedOrigins = corsOptions.AllowedOrigins;
                var exposedHeaders = corsOptions.ExposedHeaders;
                cors.AddPolicy("CorsPolicy", corsBuilder =>
                {
                    //Setting whether to handle credentials (e.g. cookies, headers)
                    if (corsOptions.AllowCredentials)
                    {
                        corsBuilder.AllowCredentials();
                    }
                    else
                    {
                        corsBuilder.DisallowCredentials();
                    }
                    //Setting allowed origins, headers and HTTP methods
                    corsBuilder
                        .WithOrigins(allowedOrigins.ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
    }
}
