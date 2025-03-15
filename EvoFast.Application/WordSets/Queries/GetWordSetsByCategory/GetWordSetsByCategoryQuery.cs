using BuildingBlocks.Pagination;
using EvoFast.Application.Dtos;

namespace EvoFast.Application.WordSets.Queries.GetWordSetsByCategory;

public record GetWordSetsByCategoryQuery(PaginationRequest PaginationRequest, Guid CategoryId) 
    : IQuery<GetWordSetsByCategoryResult>;
public record GetWordSetsByCategoryResult(PaginatedResult<WordSetDto> WordSets);