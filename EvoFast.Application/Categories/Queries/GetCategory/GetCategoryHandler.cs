using BuildingBlocks.Pagination;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.Categories.Queries.GetCategory;

public class GetCategoryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetCategoryQuery, GetCategoryResult>
{
    public async Task<GetCategoryResult> Handle(GetCategoryQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var baseQuery = dbContext.Categories;
        
        var totalCount = await baseQuery.LongCountAsync(cancellationToken);

        var categories = await baseQuery
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);;
        
        var categoryDtos = categories.Adapt<List<CategoryDto>>();
        
        return new GetCategoryResult(
            new PaginatedResult<CategoryDto>(pageIndex, pageSize, totalCount, categoryDtos));    }
}