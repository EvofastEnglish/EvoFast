using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class Category : Entity<Guid>
{
    public string Name { get; set; }
    
    public void Update(String name)
    {
        Name = name;
    }
}