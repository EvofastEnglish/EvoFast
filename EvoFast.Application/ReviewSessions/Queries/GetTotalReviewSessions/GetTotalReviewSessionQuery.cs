using EvoFast.Application.Dtos;

namespace EvoFast.Application.ReviewSessions.Queries.GetTotalReviewSessions;

public record GetTotalReviewSessionQuery : IQuery<GetTotalReviewSessionResult>;

public record GetTotalReviewSessionResult(int Total);