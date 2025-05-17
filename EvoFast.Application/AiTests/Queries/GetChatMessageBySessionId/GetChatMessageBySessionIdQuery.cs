using EvoFast.Application.Dtos;

namespace EvoFast.Application.AiTests.Queries.GetChatMessageBySessionId;

public record GetChatMessageBySessionIdQuery(Guid SessionId) : IQuery<GetAiTestSessionsResult>;
public record GetAiTestSessionsResult(List<AiTestChatMessageDto> ChatMessageDtos);