using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using EvoFast.Application.Services;
using EvoFast.Domain.Events;
using EvoFast.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.Messages.Commands.CreateMessage;

public class CreateMessageHandler(IApplicationDbContext dbContext, IChatGptService chatGptService)
    : ICommandHandler<CreateMessageCommand, CreateMessageResult>
{
    public async Task<CreateMessageResult> Handle(CreateMessageCommand command, CancellationToken cancellationToken)
    {
        var conversation = await dbContext.Conversations
            .FirstOrDefaultAsync(c => c.Id == command.MessageRequest.ConversationId, cancellationToken);
        
        var userMessage = command.MessageRequest.Adapt<Message>();
        userMessage.Role = "user";
        userMessage.CreatedAt = DateTime.UtcNow;
        dbContext.Messages.Add(userMessage);

        var messages = dbContext.Messages
            .AsNoTracking()
            .Where(m => m.ConversationId == command.MessageRequest.ConversationId)
            .OrderBy(m => m.CreatedAt)
            .ToList();
        var chatGptMessageDtos = messages.Adapt<List<ChatGptMessageDto>>();
        var (role, content) = await chatGptService.GetChatGptResponseAsync(chatGptMessageDtos);
        var assistantMessage = new Message
        {
            Id = Guid.NewGuid(),
            ConversationId = userMessage.ConversationId,
            Content = content,
            Role = role,
            CreatedAt = DateTime.UtcNow,
        };
        dbContext.Messages.Add(assistantMessage);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateMessageResult(assistantMessage.Adapt<MessageDto>());
    }
}