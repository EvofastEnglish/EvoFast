using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using EvoFast.Domain.Models;
using Mapster;

namespace EvoFast.Application.ReviewSessions.Commands.UpsertReviewSession;

public class UpsertReviewSessionHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpsertReviewSessionCommand, UpsertReviewSessionResult>
{
    public async Task<UpsertReviewSessionResult> Handle(UpsertReviewSessionCommand command, CancellationToken cancellationToken)
    {
        var reviewSession = dbContext.ReviewSessions
            .FirstOrDefault(r => r.UserId == command.UserId && r.QuestionId == command.ReviewSessionRequest.QuestionId);
        if (reviewSession == null)
        {
            reviewSession = new ReviewSession(command.UserId, command.ReviewSessionRequest.QuestionId, command.ReviewSessionRequest.IsCorrect);
            dbContext.ReviewSessions.Add(reviewSession);
        }
        else
        {
            if (command.ReviewSessionRequest.IsConfidence != null)
            {
                reviewSession.SetConfidence((bool)command.ReviewSessionRequest.IsConfidence);
            }
            reviewSession.UpdateReviewSession();
            if (reviewSession.IsDeleted)
            {
                dbContext.ReviewSessions.Remove(reviewSession);
            }
        }
        await dbContext.SaveChangesAsync(cancellationToken);
        return new UpsertReviewSessionResult(reviewSession.Adapt<ReviewSessionDto>());
    }
}