using EvoFast.Application.Dtos;

namespace EvoFast.Application.ReviewSessions.Commands.UpsertReviewSession;

public record UpsertReviewSessionCommand(UpsertReviewSessionRequest ReviewSessionRequest, Guid UserId) : ICommand<UpsertReviewSessionResult>;
public record UpsertReviewSessionRequest(Guid QuestionId);
public record UpsertReviewSessionResult(ReviewSessionDto ReviewSessionDto);