using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class AiTestResult : Entity<Guid>
{
    [ForeignKey("AiTestId")]
    public Guid AiTestId { get; set; }
    public virtual AiTest AiTest { get; set; }
    
    [ForeignKey("UserId")]
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    
    public string Evaluation { get; set; }
}