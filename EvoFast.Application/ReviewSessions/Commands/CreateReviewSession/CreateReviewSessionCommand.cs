using EvoFast.Application.Dtos;

namespace EvoFast.Application.ReviewSessions.Commands.CreateReviewSession;

public record CreateReviewSessionCommand(CreateReviewSessionRequest CreateReviewSessionRequest, Guid UserId) : ICommand<CreateReviewSessionResult>;
public record CreateReviewSessionRequest(Guid QuestionId, bool IsCorrect);
public record CreateReviewSessionResult(ReviewSessionDto ReviewSessionDto);