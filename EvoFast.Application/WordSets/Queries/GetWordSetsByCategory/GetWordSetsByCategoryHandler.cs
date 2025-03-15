using BuildingBlocks.Pagination;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.WordSets.Queries.GetWordSetsByCategory;

public class GetWordSetsByCategoryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetWordSetsByCategoryQuery, GetWordSetsByCategoryResult>
{
    public async Task<GetWordSetsByCategoryResult> Handle(GetWordSetsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        
        var baseQuery = dbContext.WordSets
            .Include(w => w.WordSetCategories)
            .Where(w => w.WordSetCategories!.Any(wc => wc.CategoryId == query.CategoryId));
        
        var totalCount = await baseQuery.LongCountAsync(cancellationToken);
        var wordSets = baseQuery
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize).ToListAsync(cancellationToken);
        
        var wordSetsDto = wordSets.Adapt<List<WordSetDto>>();
        return new GetWordSetsByCategoryResult(
            new PaginatedResult<WordSetDto>(pageIndex, pageSize, totalCount, wordSetsDto));    }
}