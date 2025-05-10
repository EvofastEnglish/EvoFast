using EvoFast.Application.Dtos;

namespace EvoFast.Application.AiTestSections.Commands.StartAiTestSection;

public record StartAiTestSectionCommand(StartAiTestSectionRequest StartAiTestSectionRequest) : ICommand<StartAiTestSectionResult>;

public record StartAiTestSectionRequest(Guid AiTestSectionId);
public record StartAiTestSectionResult(AiTestSectionResultDto AiTestSectionResult);