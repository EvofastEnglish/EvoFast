using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace EvoFast.Application.Users.EventHandlers.Integration;

public class AuthRegisterEventHandler(ISender sender, ILogger<AuthRegisterEventHandler> logger)
    : IConsumer<AuthRegisterEvent>
{
    public Task Consume(ConsumeContext<AuthRegisterEvent> context)
    {
        logger.LogInformation($"Received AuthRegisterEvent {context.Message.Id}");
        Console.WriteLine($"Received AuthRegisterEvent {context.Message}");
        return Task.CompletedTask;
    }
}