using BuildingBlocks.Pagination;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.QuestionAttempts.Queries.GetQuestionAttempts;

public class GetQuestionAttemptsHandler
    (IApplicationDbContext dbContext)
    : IQueryHandler<GetQuestionAttemptsQuery, GetQuestionAttemptsResult>
{
    public async Task<GetQuestionAttemptsResult> Handle(GetQuestionAttemptsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var baseQuery = dbContext.QuestionAttempts
            .Include(q => q.WordSetAttempt)
            .Where(q => query.UserId == q.WordSetAttempt.UserId && q.IsBookmarked);

        var totalCount = await baseQuery.LongCountAsync(cancellationToken);

        var questionAttempts = await baseQuery
            .Include(q => q.Question)
            .ThenInclude(q => q.Answers)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var questionAttemptsDto = questionAttempts.Adapt<List<QuestionAttemptDto>>();

        return new GetQuestionAttemptsResult(
            new PaginatedResult<QuestionAttemptDto>(pageIndex, pageSize, totalCount, questionAttemptsDto));    }
}