using EvoFast.Domain.Abstractions;
using EvoFast.Domain.Models;

namespace EvoFast.Domain.Events;

public record WordSetUpdatedEvent(WordSet WordSet) : IDomainEvent;