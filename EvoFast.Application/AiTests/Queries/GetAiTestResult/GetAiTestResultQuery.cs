using EvoFast.Application.Dtos;
using Microsoft.Extensions.AI;

namespace EvoFast.Application.AiTests.Queries.GetAiTestResult;

public record GetAiTestResultQuery(Guid AiTestId) : IQuery<GetAiTestResultResult>;
public record GetAiTestResultResult(List<ChatMessage> Messages);