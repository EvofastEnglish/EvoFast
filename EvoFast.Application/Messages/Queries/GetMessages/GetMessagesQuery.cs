using BuildingBlocks.Pagination;
using EvoFast.Application.Dtos;

namespace EvoFast.Application.Messages.Queries.GetMessages;

public record GetMessagesQuery(PaginationRequest PaginationRequest, Guid ConversationId)
    : IQuery<GetMessagesResult>;
public record GetMessagesResult(PaginatedResult<MessageDto> Messages);