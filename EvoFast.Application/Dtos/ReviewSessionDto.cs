namespace EvoFast.Application.Dtos;

public record ReviewSessionDto(
    DateTime MistakeDate,
    DateTime NextReviewDate,
    int ReviewStage
    );