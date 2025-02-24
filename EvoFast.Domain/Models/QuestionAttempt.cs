using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class QuestionAttempt : Entity<Guid>
{
    public bool IsBookmarked { get; set; } = false;
    
    [ForeignKey("QuestionId")]
    public Guid QuestionId { get; set; }
    public virtual Question Question { get; set; }
    
    [ForeignKey("WordSetAttemptId")]
    public Guid WordSetAttemptId { get; set; }
    public virtual WordSetAttempt WordSetAttempt { get; set; }
}