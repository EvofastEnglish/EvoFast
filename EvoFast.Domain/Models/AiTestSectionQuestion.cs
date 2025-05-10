using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class AiTestSectionQuestion : Entity<Guid>
{
    [ForeignKey("AiTestSectionId")]
    public Guid AiTestSectionId { get; set; }
    public virtual AiTestSection AiTestSection { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int ThinkingTimeSeconds { get; set; }
    public int RecordingTimeSeconds { get; set; }
    public string ChatPrompt { get; set; }
    
    public AiTestSectionQuestion(Guid aiTestSectionId, string title, string description, int thinkingTimeSeconds, int recordingTimeSeconds, string chatPrompt)
    {
        AiTestSectionId = aiTestSectionId;
        Title = title;
        Description = description;
        ThinkingTimeSeconds = thinkingTimeSeconds;
        RecordingTimeSeconds = recordingTimeSeconds;
        ChatPrompt = chatPrompt;
    }
}