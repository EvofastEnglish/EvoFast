namespace EvoFast.Application.Dtos;

public record AiTestSessionDto(
    Guid Id,
    Guid AiTestId,
    DateTime? CreatedAt,
    DateTime? CompletedAt,
    string? Summary,
    bool IsCompleted
    );