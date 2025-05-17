using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class AiTestSession : Entity<Guid>
{
    [ForeignKey("AiTestId")]
    public Guid AiTestId { get; private set; }
    public virtual AiTest AiTest { get; set; }
    
    [ForeignKey("UserId")]
    public Guid UserId { get; private set; }
    public virtual User User { get; set; }
    
    public DateTime? CompletedAt { get; private set; }
    public string? Summary { get; private set; }
    public bool IsCompleted { get; private set; } = false;

    public virtual ICollection<AiTestChatMessage> AiTestChatMessages { get; set; }
    
    public AiTestSession(Guid aiTestId, Guid userId)
    {
        Id = Guid.NewGuid();
        AiTestId = aiTestId;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }
    
    public void Complete(string summary)
    {
        if (CompletedAt.HasValue || IsCompleted)
        {
            throw new InvalidOperationException("Session has already been completed.");
        }
        CompletedAt = DateTime.UtcNow;
        Summary = summary;
        IsCompleted = true;
    }
}