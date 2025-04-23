using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class WordSetAttempt : Entity<Guid>
{
    [ForeignKey("UserId")]
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    
    [ForeignKey("WordSetId")]
    public Guid WordSetId { get; set; }
    public virtual WordSet WordSet { get; set; }
    public DateTime? CompletedAt { get; set; }
}