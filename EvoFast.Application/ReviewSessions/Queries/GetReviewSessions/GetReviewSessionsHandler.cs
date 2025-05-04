using BuildingBlocks.Pagination;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.ReviewSessions.Queries.GetReviewSessions;

public class GetReviewSessionsHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetReviewSessionsQuery, GetReviewSessionsResult>
{
    public async Task<GetReviewSessionsResult> Handle(GetReviewSessionsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var baseQuery = dbContext.ReviewSessions;

        var totalCount = await baseQuery.LongCountAsync(cancellationToken);

        var reviewSessions = await baseQuery
            .Include(q => q.Question)
            .ThenInclude(q => q.Answers)
            .OrderByDescending(q => q.NextReviewDate)
            .ThenByDescending(q => q.MistakeDate)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var reviewSessionDtos = reviewSessions.Adapt<List<ReviewSessionDto>>();

        return new GetReviewSessionsResult(
            new PaginatedResult<ReviewSessionDto>(pageIndex, pageSize, totalCount, reviewSessionDtos));
    }
}