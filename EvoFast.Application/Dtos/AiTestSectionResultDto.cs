namespace EvoFast.Application.Dtos;

public record AiTestSectionResultDto(
    Guid Id,
    Guid AiTestResultId,
    Guid AiTestSectionId,
    string Evaluation
    );