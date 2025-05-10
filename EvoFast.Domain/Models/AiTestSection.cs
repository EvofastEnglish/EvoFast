using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class AiTestSection : Entity<Guid>
{
    [ForeignKey("AiTestId")]
    public Guid AiTestId { get; set; }
    public virtual AiTest AiTest { get; set; }
    public int SectionOrder { get; set; }
    public string Title { get; set; }
    public int TotalQuestion { get; set; }
    public string EvaluationCriteria { get; set; }
    public string Description { get; set; }
    public string ChatPrompt { get; set; }
    
    public AiTestSection(Guid aiTestId, int sectionOrder, string title, int totalQuestion, string evaluationCriteria, string description, string chatPrompt)
    {
        AiTestId = aiTestId;
        SectionOrder = sectionOrder;
        Title = title;
        TotalQuestion = totalQuestion;
        EvaluationCriteria = evaluationCriteria;
        Description = description;
        ChatPrompt = chatPrompt;
    }
}