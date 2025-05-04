using BuildingBlocks.Pagination;
using EvoFast.Application.Dtos;

namespace EvoFast.Application.ReviewSessions.Queries.GetReviewSessions;

public record GetReviewSessionsQuery(PaginationRequest PaginationRequest)
    : IQuery<GetReviewSessionsResult>;
public record GetReviewSessionsResult(PaginatedResult<ReviewSessionDto> ReviewSessions);