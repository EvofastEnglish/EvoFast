using BuildingBlocks.Exceptions;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;

namespace EvoFast.Application.ReviewSessions.Commands.UpsertReviewSession;

public class UpsertReviewSessionHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpsertReviewSessionCommand, UpsertReviewSessionResult>
{
    public async Task<UpsertReviewSessionResult> Handle(UpsertReviewSessionCommand command, CancellationToken cancellationToken)
    {
        var reviewSession = dbContext.ReviewSessions
            .FirstOrDefault(r => r.QuestionId == command.ReviewSessionRequest.QuestionId);
        if (reviewSession == null)
        {
            throw new NotFoundException("ReviewSession");
        }
        reviewSession.UpdateReviewSession();
        if (reviewSession.IsDeleted)
        {
            dbContext.ReviewSessions.Remove(reviewSession);
        }
        await dbContext.SaveChangesAsync(cancellationToken);
        return new UpsertReviewSessionResult(reviewSession.Adapt<ReviewSessionDto>());
    }
    
    public class UpsertReviewSessionCommandValidator : AbstractValidator<UpsertReviewSessionCommand>
    {
        public UpsertReviewSessionCommandValidator()
        {
            RuleFor(x => x.ReviewSessionRequest.QuestionId).NotEmpty().WithMessage("QuestionId is required.");

        }
    }
}