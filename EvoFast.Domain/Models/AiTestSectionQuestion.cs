using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class AiTestSectionQuestion : Entity<Guid>
{
    [ForeignKey("AiTestSectionId")]
    public Guid AiTestSectionId { get; set; }
    public virtual AiTestSection AiTestSection { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int ThinkingTimeSeconds { get; set; }
    public int RecordingTimeSeconds { get; set; }
}