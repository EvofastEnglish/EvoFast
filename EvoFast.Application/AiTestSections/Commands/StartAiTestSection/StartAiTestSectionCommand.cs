using EvoFast.Application.Dtos;

namespace EvoFast.Application.AiTestSections.Commands.StartAiTestSection;

public record StartAiTestSectionCommand(StartAiTestSectionRequest StartAiTestSectionRequest) : ICommand<StartAiTestSectionResult>;

public record StartAiTestSectionRequest(Guid AiTestSessionId, Guid AiTestSectionId, Guid QuestionId);
public record StartAiTestSectionResult(List<AiTestChatMessageDto>  ChatMessageDtos);