using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;
using EvoFast.Domain.Events;

namespace EvoFast.Domain.Models;

public class Message : Aggregate<Guid>
{
    public string Role { get; set; }
    public string Content { get; set; }
    [ForeignKey("ConversationId")]
    public Guid ConversationId { get; set; }
    public virtual Conversation Conversation { get; set; }
    
    public static Message Create(String content, Guid conversationId)
    {
        var message = new Message
        {
            Id = Guid.NewGuid(),
            Role = "user",
            Content = content,
            ConversationId = conversationId,
            CreatedAt = DateTime.UtcNow,
        };
        message.AddDomainEvent(new MessageCreatedEvent(message));
        return message;
    }
}