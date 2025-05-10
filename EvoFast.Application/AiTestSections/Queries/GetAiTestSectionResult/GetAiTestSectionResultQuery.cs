using EvoFast.Application.Dtos;

namespace EvoFast.Application.AiTestSections.Queries.GetAiTestSectionResult;

public record GetAiTestSectionResultQuery(Guid AiTestSectionId) : IQuery<GetAiTestSectionResultResult>;
public record GetAiTestSectionResultResult(AiTestSectionResultDto AiTestSectionResult);