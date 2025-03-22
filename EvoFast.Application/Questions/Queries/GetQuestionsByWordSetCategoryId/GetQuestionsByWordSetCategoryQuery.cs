using BuildingBlocks.Pagination;
using EvoFast.Application.Dtos;

namespace EvoFast.Application.Questions.Queries.GetQuestionsByWordSetCategoryId;

public record GetQuestionsByWordSetCategoryQuery(PaginationRequest PaginationRequest, Guid WordSetCategoryId) 
    : IQuery<GetQuestionsByWordSetCategoryResult>;
public record GetQuestionsByWordSetCategoryResult(PaginatedResult<QuestionDto> Questions);