using EvoFast.Application.Dtos;

namespace EvoFast.Application.AiTests.Commands.CompleteAiTest;

public record CompleteAiTestCommand(CompleteAiTestRequest CompleteAiTestRequest, Guid UserId) : ICommand<CompleteAiTestResult>;
public record CompleteAiTestRequest(Guid AiTestSessionId);
public record CompleteAiTestResult(AiTestSessionDto AiTestSessionDto);