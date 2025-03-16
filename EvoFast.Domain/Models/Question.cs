using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class Question : Aggregate<Guid>
{
    public string Name { get; set; }
    
    [ForeignKey("WordSetId")]
    public Guid WordSetId { get; set; }
    public virtual WordSet WordSet { get; set; }
    
    private readonly List<Answer> _answers = new();
    public IReadOnlyList<Answer> Answers => _answers.AsReadOnly();
}