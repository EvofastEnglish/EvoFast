using BuildingBlocks.Pagination;
using EvoFast.Application.Dtos;

namespace EvoFast.Application.QuestionAttempts.Queries.GetQuestionAttempts;

public record GetQuestionAttemptsQuery(PaginationRequest PaginationRequest, Guid UserId)
    : IQuery<GetQuestionAttemptsResult>;
public record GetQuestionAttemptsResult(PaginatedResult<QuestionAttemptDto> QuestionAttempts);