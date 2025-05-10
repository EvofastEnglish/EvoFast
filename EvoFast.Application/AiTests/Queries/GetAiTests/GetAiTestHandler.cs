using BuildingBlocks.Pagination;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.AiTests.Queries.GetAiTests;

public class GetAiTestHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetAiTestsQuery, GetAiTestsQueryResult>
{
    public async Task<GetAiTestsQueryResult> Handle(GetAiTestsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var baseQuery = dbContext.AiTests.Include(a => a.AiTestSections.OrderBy(s => s.SectionOrder));
        
        var totalCount = await baseQuery.LongCountAsync(cancellationToken);

        var aiTests = await baseQuery
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);;
        
        var aiTestDtos = aiTests.Adapt<List<AiTestDto>>();
        
        return new GetAiTestsQueryResult(
            new PaginatedResult<AiTestDto>(pageIndex, pageSize, totalCount, aiTestDtos));    
    }
}