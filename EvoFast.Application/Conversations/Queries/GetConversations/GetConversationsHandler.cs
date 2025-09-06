using BuildingBlocks.Pagination;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.Conversations.Queries.GetConversations;

public class GetConversationsHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetConversationsQuery, GetConversationsResult>
{
    public async Task<GetConversationsResult> Handle(GetConversationsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        var totalCount = await dbContext.Conversations.LongCountAsync(cancellationToken);
        var conversations = dbContext.Conversations
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
        var conversationsDto = conversations.Adapt<List<ConversationDto>>();
        return new GetConversationsResult(
            new PaginatedResult<ConversationDto>(pageIndex, pageSize, totalCount, conversationsDto));    
    }
}