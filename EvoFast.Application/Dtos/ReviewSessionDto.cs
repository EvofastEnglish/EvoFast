namespace EvoFast.Application.Dtos;

public record ReviewSessionDto(
    Guid Id,
    DateTime MistakeDate,
    DateTime NextReviewDate,
    int ReviewStage,
    bool IsConfidence,
    QuestionDto? Question
    );