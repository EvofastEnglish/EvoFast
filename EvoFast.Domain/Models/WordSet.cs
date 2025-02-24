using EvoFast.Domain.Abstractions;
using EvoFast.Domain.Events;
using EvoFast.Domain.ValueObjects;

namespace EvoFast.Domain.Models;

public class WordSet : Aggregate<Guid>
{
    public long NumberId { get; set; }
    public static WordSet Create(Guid wordSetId, long numberId)
    {
        var wordSet = new WordSet { Id = wordSetId, NumberId = numberId };
        wordSet.AddDomainEvent(new WordSetCreatedEvent(wordSet));
        return wordSet;
    }

    public void Update(long numberId)
    {
        NumberId = numberId;
        AddDomainEvent(new WordSetUpdatedEvent(this));
    }
}