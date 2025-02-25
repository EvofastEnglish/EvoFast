using BuildingBlocks.Pagination;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.Questions.Queries.GetQuestionsByWordSet;

public class GetQuestionByWordSetHandler
    (IApplicationDbContext dbContext)
    : IQueryHandler<GetQuestionsByWordSetQuery, GetQuestionsByWordSetResult>
{
    public async Task<GetQuestionsByWordSetResult> Handle(GetQuestionsByWordSetQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var baseQuery = dbContext.Questions
            .Include(q => q.Answers)
            .Where(q => q.WordSetId == query.WordSetId);
        
        var totalCount = await baseQuery.LongCountAsync(cancellationToken);

        var questions = await baseQuery
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);;
        
        var questionDtos = questions.Adapt<List<QuestionDto>>();
        
        return new GetQuestionsByWordSetResult(
            new PaginatedResult<QuestionDto>(pageIndex, pageSize, totalCount, questionDtos));
    }
}