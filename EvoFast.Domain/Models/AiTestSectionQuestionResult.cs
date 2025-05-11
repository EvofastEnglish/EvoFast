using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class AiTestSectionQuestionResult : Entity<Guid>
{
    [ForeignKey("AiTestSectionResultId")]
    public Guid AiTestSectionResultId { get; set; }
    public virtual AiTestSectionResult AiTestSectionResult { get; set; }
    
    [ForeignKey("AiTestSectionQuestionId")]
    public Guid AiTestSectionQuestionId { get; set; }
    public virtual AiTestSectionQuestion AiTestSectionQuestion { get; set; }
    public string Evaluation { get; set; }
    public string ChatPrompt { get; set; }
}