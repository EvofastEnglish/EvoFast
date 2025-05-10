using BuildingBlocks.Pagination;
using EvoFast.Application.Dtos;

namespace EvoFast.Application.AiTests.Queries.GetAiTests;

public record GetAiTestsQuery(PaginationRequest PaginationRequest) : IQuery<GetAiTestsQueryResult>;
public record GetAiTestsQueryResult(PaginatedResult<AiTestDto> AiTests);