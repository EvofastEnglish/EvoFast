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
        string conversationLanguage = conversation.Language switch
        {
            "ja" => "Japanese",
            "en" => "English",
            _ => "English"
        };
        var languageInstruction = $"Please respond in {conversationLanguage} only. ";
        var prompt = $"You are an AI assistant. The user's role is {conversation.YourRole}. " +
                     $"The AI's role is {conversation.AIRole}. " +
                     $"The topic of the conversation is {conversation.Topic}. " +
                     $"{languageInstruction}" +
                     "The user is learning this language, so adapt your responses accordingly (e.g., vocabulary level, clarity). " +
                     "The conversation should be dynamic, with short, clear responses that promote dialogue. " +
                     "Your responses should be concise, and when appropriate, ask follow-up questions to keep the conversation flowing. " +
                     "Maintain an engaging and natural back-and-forth between the user and the AI.";
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