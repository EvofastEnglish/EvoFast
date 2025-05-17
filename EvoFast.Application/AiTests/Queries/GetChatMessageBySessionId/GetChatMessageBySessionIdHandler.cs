using BuildingBlocks.Exceptions;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.AiTests.Queries.GetChatMessageBySessionId;

public class GetChatMessageBySessionIdHandler(IApplicationDbContext dbContext)
: IQueryHandler<GetChatMessageBySessionIdQuery, GetAiTestSessionsResult>
{
    public async Task<GetAiTestSessionsResult> Handle(GetChatMessageBySessionIdQuery query, CancellationToken cancellationToken)
    {
        var session = dbContext.AiTestSessions
            .Include(_ => _.AiTestChatMessages)
            .FirstOrDefault(s =>
                s.Id == query.SessionId);

        if (session == null)
        {
            throw new NotFoundException("AiTestSession", query.SessionId);
        }    
        var messageDtos = session.AiTestChatMessages.Adapt<List<AiTestChatMessageDto>>();
        return new GetAiTestSessionsResult(messageDtos);
    }
}