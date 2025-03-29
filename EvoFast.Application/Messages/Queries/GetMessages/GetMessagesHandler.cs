using BuildingBlocks.Pagination;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.Messages.Queries.GetMessages;

public class GetMessagesHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetMessagesQuery, GetMessagesResult>
{
    public async Task<GetMessagesResult> Handle(GetMessagesQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var baseQuery = dbContext.Messages
            .AsNoTracking()
            .Where(m => m.ConversationId == query.ConversationId);
        var totalCount = await baseQuery.LongCountAsync(cancellationToken);
        var messages = baseQuery
            .OrderBy(m => m.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
        var messageDtos  = messages.Adapt<List<MessageDto>>();
        return new GetMessagesResult(
            new PaginatedResult<MessageDto>(pageIndex, pageSize, totalCount, messageDtos));      
    }
}