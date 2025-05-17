namespace EvoFast.Application.Dtos;

public record AiTestSectionDto(
    Guid Id,
    Guid AiTestId,
    int SectionOrder,
    string Title,
    int TotalQuestion,
    string EvaluationCriteria,
    string Description,
    List<AiTestSectionQuestionDto> Questions
    );