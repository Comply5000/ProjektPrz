using System.Collections.Immutable;
using System.Reflection;
using API.Common.MessageBroker.Config;
using API.Features.Identity.Events.SendConfirmAccountEmail;
using MassTransit;
using Microsoft.Extensions.Options;

namespace API.Extensions;

public static class MessageBrokerExtension
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessageBrokerConfiguration>(
            configuration.GetSection("MessageBroker"));

        services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<MessageBrokerConfiguration>>().Value);

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.AddConsumers(Assembly.GetExecutingAssembly());

            busConfigurator.UsingRabbitMq((context, config) =>
            {
                var settings = context.GetRequiredService<MessageBrokerConfiguration>();

                config.Host(new Uri(settings.Host), h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });
                
                config.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(10)));
                
                config.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}