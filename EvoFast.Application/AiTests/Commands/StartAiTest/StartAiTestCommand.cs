using EvoFast.Application.Dtos;

namespace EvoFast.Application.AiTests.Commands.StartAiTest;

public record StartAiTestCommand(StartAiTestRequest StartAiTestRequest, Guid UserId) : ICommand<StartAiTestResult>;
public record StartAiTestRequest(Guid AiTestId);
public record StartAiTestResult(AiTestSessionDto AiTestSessionDto);