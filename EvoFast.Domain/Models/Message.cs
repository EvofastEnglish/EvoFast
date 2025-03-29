using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class Message : Entity<Guid>
{
    public string Role { get; set; }
    public string Content { get; set; }
    [ForeignKey("ConversationId")]
    public Guid ConversationId { get; set; }
    public virtual Conversation Conversation { get; set; }
}