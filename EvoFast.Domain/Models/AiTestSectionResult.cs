using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class AiTestSectionResult : Entity<Guid>
{
    [ForeignKey("AiTestResultId")]
    public Guid AiTestResultId { get; set; }
    public virtual AiTestResult AiTestResult { get; set; }
    
    [ForeignKey("AiTestSectionId")]
    public Guid AiTestSectionId { get; set; }
    public virtual AiTestSection AiTestSection { get; set; }
    
    public string Evaluation { get; set; }
    public string ChatPrompt { get; set; }
}