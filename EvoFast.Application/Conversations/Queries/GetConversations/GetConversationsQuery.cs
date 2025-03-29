using BuildingBlocks.Pagination;
using EvoFast.Application.Dtos;

namespace EvoFast.Application.Conversations.Queries.GetConversations;

public record GetConversationsQuery(PaginationRequest PaginationRequest)
    : IQuery<GetConversationsResult>;
public record GetConversationsResult(PaginatedResult<ConversationDto> Conversations);