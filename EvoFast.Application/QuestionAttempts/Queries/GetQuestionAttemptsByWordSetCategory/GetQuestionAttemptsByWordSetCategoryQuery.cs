using BuildingBlocks.Pagination;
using EvoFast.Application.Dtos;

namespace EvoFast.Application.QuestionAttempts.Queries.GetQuestionAttemptsByWordSetCategory;

public record GetQuestionAttemptsByWordSetCategoryQuery(PaginationRequest PaginationRequest, Guid UserId, Guid WordSetCategoryId)
    : IQuery<GetQuestionAttemptsByWordSetCategoryResult>;
public record GetQuestionAttemptsByWordSetCategoryResult(PaginatedResult<QuestionAttemptDto> QuestionAttempts);