using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class AiTest : Entity<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ChatPromptStart { get; set; }
    public string ChatPromptFinish { get; set; }
}