using BuildingBlocks.Pagination;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.WordSets.Queries.GetWordSets;

public class GetWordSetsHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetWordSetsQuery, GetWordSetsResult>
{
    public async Task<GetWordSetsResult> Handle(GetWordSetsQuery request, CancellationToken cancellationToken)
    {
        var pageIndex = request.PaginationRequest.PageIndex;
        var pageSize = request.PaginationRequest.PageSize;
        var totalCount = await dbContext.WordSets.LongCountAsync(cancellationToken);
        var wordSets = dbContext.WordSets
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
        var wordSetsDto = wordSets.Adapt<List<WordSetDto>>();
        return new GetWordSetsResult(
            new PaginatedResult<WordSetDto>(pageIndex, pageSize, totalCount, wordSetsDto));
    }
}