using API.Common.MessageBroker.Services;
using API.Common.SMTP.Config;
using API.Common.SMTP.Services;

namespace API.Common;

public static class CommonExtensions
{
    public static IServiceCollection AddCommon(this IServiceCollection services, IConfiguration configuration)
    {
        var s3Config = new SmtpConfig();
        configuration.GetSection("SMTP").Bind(s3Config);
        services.AddSingleton(s3Config);
        
        services.AddScoped<IEmailSenderService, EmailSenderService>();
        services.AddTransient<IEventBus, EventBus>();

        return services;
    }
}