using EvoFast.Domain.Abstractions;
using EvoFast.Domain.Events;
using EvoFast.Domain.ValueObjects;

namespace EvoFast.Domain.Models;

public class WordSet : Entity<Guid>
{
    public long NumberId { get; set; }
    public virtual ICollection<Question>? Questions { get; set; }
    public virtual ICollection<WordSetCategory>? WordSetCategories { get; set; }
    public static WordSet Create(Guid wordSetId, long numberId)
    {
        var wordSet = new WordSet { Id = wordSetId, NumberId = numberId };
        return wordSet;
    }

    public void Update(long numberId)
    {
        NumberId = numberId;
    }
}