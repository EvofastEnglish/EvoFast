namespace EvoFast.Application.Dtos;

public class ConversationDto
{
    public Guid Id { get; set; }
    public string YourRole { get; set; }
    public string AIRole { get; set; }
    public bool Topic { get; set; }
    public DateTime CreatedAt { get; set; }
}