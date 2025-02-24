using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class Answer : Entity<Guid>
{
    public string Name { get; set; }
    public string TranslatedName { get; set; }
    public bool IsCorrect { get; set; } = false;
    
    [ForeignKey("QuestionId")]
    public Guid QuestionId { get; set; }
    public virtual Question Question { get; set; }
}