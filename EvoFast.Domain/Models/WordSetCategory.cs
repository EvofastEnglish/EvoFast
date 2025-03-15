using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class WordSetCategory : Entity<Guid>
{
    [ForeignKey("CategoryId")]
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }
    
    [ForeignKey("WordSetId")]
    public Guid WordSetId { get; set; }
    public virtual WordSet WordSet { get; set; }
}