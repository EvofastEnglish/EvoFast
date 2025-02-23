using BuildingBlocks.Messaging.Events;
using EvoFast.Application.Users.Commands.CreateUser;
using MassTransit;

namespace EvoFast.Application.Users.EventHandlers.Integration;

public class AuthRegisterEventHandler
    (ISender sender, ILogger<AuthRegisterEventHandler> logger)
    : IConsumer<AuthRegisterEvent>
{
    public async Task Consume(ConsumeContext<AuthRegisterEvent> context)
    {
        logger.LogInformation($"Received AuthRegisterEvent {context.Message.Id}");
        var request = new CreateUserRequest(
            context.Message.UserId,
            context.Message.Email,
            context.Message.Username,
            context.Message.FirstName,
            context.Message.LastName);
        var command = new CreateUserCommand(request);
        await sender.Send(command);
    }
}