namespace EvoFast.Application.Dtos;

public record AiTestSectionQuestionResultDto(
    Guid Id,
    Guid AiTestSectionResultId,
    Guid AiTestSectionQuestionId,
    string Evaluation,
    string ChatPrompt
    );