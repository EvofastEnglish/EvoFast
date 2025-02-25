using BuildingBlocks.Pagination;
using EvoFast.Application.Dtos;
using EvoFast.Application.QuestionAttempts.Queries.GetQuestionAttempts;

namespace EvoFast.Application.QuestionAttempts.Queries.GetQuestionAttemptsByWordSet;

public record GetQuestionAttemptsByWordSetQuery(PaginationRequest PaginationRequest, Guid UserId, Guid WordSetId)
    : IQuery<GetQuestionAttemptsByWordSetResult>;
public record GetQuestionAttemptsByWordSetResult(PaginatedResult<QuestionAttemptDto> QuestionAttempts);