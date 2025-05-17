namespace EvoFast.Application.Dtos;

public record AiTestChatMessageDto(
    string Role,
    string Content,
    DateTime CreatedAt
    );