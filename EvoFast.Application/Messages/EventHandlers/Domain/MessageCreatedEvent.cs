using EvoFast.Domain.Events;

namespace EvoFast.Application.Messages.EventHandlers.Domain;

public class MessageCreatedEventHandler(
    ILogger<MessageCreatedEventHandler> logger)
    : INotificationHandler<MessageCreatedEvent>
{
    public async Task Handle(MessageCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);
    }
}