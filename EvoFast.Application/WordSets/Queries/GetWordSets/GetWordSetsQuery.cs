using BuildingBlocks.Pagination;
using EvoFast.Application.Dtos;

namespace EvoFast.Application.WordSets.Queries.GetWordSets;

public record GetWordSetsQuery(PaginationRequest PaginationRequest)
    : IQuery<GetWordSetsResult>;
public record GetWordSetsResult(PaginatedResult<WordSetDto> WordSets);