using EvoFast.Application.Dtos;

namespace EvoFast.Application.ReviewSessions.Commands.UpsertReviewSession;

public record UpsertReviewSessionCommand(UpsertReviewSessionRequest ReviewSessionRequest, Guid UserId) : ICommand<UpsertReviewSessionResult>;
public record UpsertReviewSessionRequest(Guid QuestionId, bool IsCorrect, bool? IsConfidence);
public record UpsertReviewSessionResult(ReviewSessionDto ReviewSessionDto);