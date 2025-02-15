using EvoFast.Domain.Abstractions;
using EvoFast.Domain.Events;
using EvoFast.Domain.ValueObjects;

namespace EvoFast.Domain.Models;

public class WordSet : Aggregate<WordSetId>
{
    public WordSetName WordSetName { get; private set; }
    public string? Description { get; set; }

    public static WordSet Create(WordSetId wordSetId, WordSetName wordSetName, string? description)
    {
        var wordSet = new WordSet { Id = wordSetId, WordSetName = wordSetName, Description = description };
        wordSet.AddDomainEvent(new WordSetCreatedEvent(wordSet));
        return wordSet;
    }

    public void Update(WordSetName wordSetName, string? description)
    {
        WordSetName = wordSetName;
        Description = description;
        AddDomainEvent(new WordSetUpdatedEvent(this));
    }
}