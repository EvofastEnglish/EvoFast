using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using EvoFast.Application.Services;
using EvoFast.Domain.Models;
using Mapster;
using Microsoft.Extensions.Options;

namespace EvoFast.Application.Conversations.Commands.AddConversation;

public class CreateConversationHandler(IApplicationDbContext dbContext, IChatGptService chatGptService, IOptions<Dictionary<string, string>> languageOptions)
: ICommandHandler<CreateConversationCommand, CreateConversationResult>
{
    private readonly Dictionary<string, string> _languageNames = languageOptions.Value;
    public async Task<CreateConversationResult> Handle(CreateConversationCommand command, CancellationToken cancellationToken)
    {
        var conversation = command.Conversation.Adapt<Conversation>();
        conversation.CreatedAt = DateTime.UtcNow;
        dbContext.Conversations.Add(conversation);

        var chatGptMessageDtos = new List<ChatGptMessageDto>();
        
        string languageInstruction = "";
        if (conversation.Language != "en" && _languageNames.TryGetValue(conversation.Language, out var languageName))
        {
            languageInstruction = $"Please respond in {languageName} language only. ";
        }
        var prompt = $"You are an AI assistant. The user's role is {conversation.YourRole}. " +
                     $"The AI's role is {conversation.AIRole}. " +
                     $"The topic of the conversation is {conversation.Topic}. " +
                     $"{languageInstruction} " +
                     "Please keep your responses short, clear, and focused on the conversation. " +
                     "Avoid long explanations, and use simple, concise sentences to keep the dialogue flowing naturally.";
        chatGptMessageDtos.Add(new ChatGptMessageDto("system", prompt));

        var systemMessage = new Message
        {
            Id = Guid.NewGuid(),
            ConversationId = conversation.Id,
            Content = prompt,
            Role = "system",
            CreatedAt = DateTime.UtcNow
        };
        dbContext.Messages.Add(systemMessage);
        
        var (role, content) = await chatGptService.GetChatGptResponseAsync(chatGptMessageDtos);
        var assistantMessage = new Message
        {
            Id = Guid.NewGuid(),
            ConversationId = conversation.Id,
            Content = content,
            Role = role,
            CreatedAt = DateTime.UtcNow,
        };
        dbContext.Messages.Add(assistantMessage);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateConversationResult(conversation.Id);    
    }
}