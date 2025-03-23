using BuildingBlocks.Pagination;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.QuestionAttempts.Queries.GetQuestionAttemptsByWordSetCategory;

public class GetQuestionAttemptsByWordSetCategoryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetQuestionAttemptsByWordSetCategoryQuery, GetQuestionAttemptsByWordSetCategoryResult>
{
    public async Task<GetQuestionAttemptsByWordSetCategoryResult> Handle(GetQuestionAttemptsByWordSetCategoryQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var baseQuery = dbContext.QuestionAttempts
            .Include(q => q.WordSetAttempt)
            .Where(q => 
                query.UserId == q.WordSetAttempt.UserId 
                && query.WordSetCategoryId == q.WordSetAttempt.WordSetId 
                && q.IsBookmarked);

        var totalCount = await baseQuery.LongCountAsync(cancellationToken);

        var questionAttempts = await baseQuery
            .Include(q => q.Question)
            .ThenInclude(q => q.Answers)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var questionAttemptsDto = questionAttempts.Adapt<List<QuestionAttemptDto>>();

        return new GetQuestionAttemptsByWordSetCategoryResult(
            new PaginatedResult<QuestionAttemptDto>(pageIndex, pageSize, totalCount, questionAttemptsDto));    }
}