using EvoFast.Domain.Events;

namespace EvoFast.Application.WordSets.EventHandlers;

public class WordSetCreatedEventHandler(ILogger<WordSetCreatedEventHandler> logger)
    : INotificationHandler<WordSetCreatedEvent>
{
    public Task Handle(WordSetCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Handled: {domainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}