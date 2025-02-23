using EvoFast.Domain.Abstractions;
using EvoFast.Domain.Models;

namespace EvoFast.Domain.Events;

public class UserCreatedEvent(User User) : IDomainEvent;