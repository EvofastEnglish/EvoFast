using BuildingBlocks.Exceptions;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;

namespace EvoFast.Application.ReviewSessions.Commands.UpdateConfidenceReviewSession;

public class UpdateConfidenceReviewSessionHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateConfidenceReviewSessionCommand, UpdateConfidenceReviewSessionResult>
{
    public async Task<UpdateConfidenceReviewSessionResult> Handle(UpdateConfidenceReviewSessionCommand command, CancellationToken cancellationToken)
    {
        var reviewSession = dbContext.ReviewSessions
            .FirstOrDefault(r => r.UserId == command.UserId && r.QuestionId == command.UpdateConfidenceReviewSessionRequest.QuestionId);
        if (reviewSession == null)
        {
            throw new NotFoundException("ReviewSession");
        }
        reviewSession?.SetConfidence(command.UpdateConfidenceReviewSessionRequest.IsConfidence);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new UpdateConfidenceReviewSessionResult(reviewSession.Adapt<ReviewSessionDto>());
    }
    
    public class UpdateConfidenceReviewSessionCommandValidator : AbstractValidator<UpdateConfidenceReviewSessionCommand>
    {
        public UpdateConfidenceReviewSessionCommandValidator()
        {
            RuleFor(x => x.UpdateConfidenceReviewSessionRequest.QuestionId).NotEmpty().WithMessage("QuestionId is required.");
            RuleFor(x => x.UpdateConfidenceReviewSessionRequest.IsConfidence).NotEmpty().WithMessage("IsConfidence is required.");

        }
    }
}