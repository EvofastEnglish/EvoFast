using BuildingBlocks.Pagination;
using EvoFast.Application.Dtos;

namespace EvoFast.Application.Categories.Queries.GetCategory;

public record GetCategoryQuery(PaginationRequest PaginationRequest) 
    : IQuery<GetCategoryResult>;
public record GetCategoryResult(PaginatedResult<CategoryDto> Categories);