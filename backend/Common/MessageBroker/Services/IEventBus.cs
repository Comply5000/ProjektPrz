namespace API.Common.MessageBroker.Services;

public interface IEventBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken) where T : class;
}