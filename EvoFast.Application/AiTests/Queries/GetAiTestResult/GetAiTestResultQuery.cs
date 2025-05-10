using EvoFast.Application.Dtos;

namespace EvoFast.Application.AiTests.Queries.GetAiTestResult;

public record GetAiTestResultQuery(Guid AiTestId) : IQuery<GetAiTestResultResult>;
public record GetAiTestResultResult(AiTestResultDto AiTestResult);