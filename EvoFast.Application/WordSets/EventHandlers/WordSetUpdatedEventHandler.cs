using EvoFast.Domain.Events;

namespace EvoFast.Application.WordSets.EventHandlers;

public class WordSetUpdatedEventHandler(ILogger<WordSetUpdatedEventHandler> logger)
    : INotificationHandler<WordSetUpdatedEvent>
{
    public Task Handle(WordSetUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Handled: {domainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}