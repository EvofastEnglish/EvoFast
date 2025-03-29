using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using EvoFast.Domain.Models;
using Mapster;

namespace EvoFast.Application.Messages.Commands.CreateMessage;

public class CreateMessageHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateMessageCommand, CreateMessageResult>
{
    public async Task<CreateMessageResult> Handle(CreateMessageCommand command, CancellationToken cancellationToken)
    {
        var newMessage = Message.Create(
            command.MessageRequest.Content,
            command.MessageRequest.ConversationId);
        dbContext.Messages.Add(newMessage);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateMessageResult(newMessage.Adapt<MessageDto>());
    }
}