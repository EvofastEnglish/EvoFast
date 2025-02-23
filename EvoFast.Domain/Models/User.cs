using EvoFast.Domain.Abstractions;
using EvoFast.Domain.Events;

namespace EvoFast.Domain.Models;

public class User : Aggregate<Guid>
{
    public required string Email { get; set; }
    public required string Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public static User Create(Guid id, string email, string username, string? firstName, string? lastName)
    {
        var user = new User
        {
            Id = id, 
            Email = email, 
            Username = username, 
            FirstName = firstName, 
            LastName = lastName
        };
        user.AddDomainEvent(new UserCreatedEvent(user));
        return user;
    }
}