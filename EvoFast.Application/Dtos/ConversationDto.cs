namespace EvoFast.Application.Dtos;

public record ConversationDto(
    Guid Id, 
    string YourRole,
    string AIRole,
    string Topic,
    string Language,
    DateTime CreatedAt);