namespace EvoFast.Application.ReviewSessions.Commands.DeleteRevewSession;

public record DeleteReviewSessionCommand(Guid Id) : ICommand<DeleteReviewSessionResult>;
public record DeleteReviewSessionResult(bool IsSuccess);