using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;
using Microsoft.Extensions.AI;

namespace EvoFast.Domain.Models;

public class AiTestChatMessage : Entity<Guid>
{ 
    [ForeignKey("AiTestSessionId")]
    public Guid AiTestSessionId { get; private set; }
    public virtual AiTestSession AiTestSession { get; set; }
    public string Role { get; private set; }
    public string Content { get; private set; }
    public string? TranscribeAudio { get; set; }
    
    private AiTestChatMessage() { }
    public AiTestChatMessage(Guid sessionId, string role, string content, string? transcribeAudio = null)
    {
        Id = Guid.NewGuid();
        AiTestSessionId = sessionId;
        Role = role;
        Content = content;
        CreatedAt = DateTime.UtcNow;
        TranscribeAudio = transcribeAudio;
    }
}