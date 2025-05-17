namespace EvoFast.Application.Dtos;

public record AiTestSectionQuestionDto(
    Guid Id, 
    string Title,
    string Description,
    int ThinkingTimeSeconds,
    int RecordingTimeSeconds
    );