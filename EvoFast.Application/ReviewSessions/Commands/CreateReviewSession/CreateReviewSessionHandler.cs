using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using EvoFast.Domain.Models;
using Mapster;

namespace EvoFast.Application.ReviewSessions.Commands.CreateReviewSession;

public class CreateReviewSessionHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateReviewSessionCommand, CreateReviewSessionResult>
{
    public async Task<CreateReviewSessionResult> Handle(CreateReviewSessionCommand command, CancellationToken cancellationToken)
    {
        var reviewSession = dbContext.ReviewSessions
            .FirstOrDefault(r => r.UserId == command.UserId && r.QuestionId == command.CreateReviewSessionRequest.QuestionId);
        if (reviewSession == null)
        {
            reviewSession = new ReviewSession(command.UserId, command.CreateReviewSessionRequest.QuestionId, command.CreateReviewSessionRequest.IsCorrect);
            dbContext.ReviewSessions.Add(reviewSession);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        return new CreateReviewSessionResult(reviewSession.Adapt<ReviewSessionDto>());    
    }
}

public class CreateReviewSessionCommandCommandValidator : AbstractValidator<CreateReviewSessionCommand>
{
    public CreateReviewSessionCommandCommandValidator()
    {
        RuleFor(x => x.CreateReviewSessionRequest.QuestionId).NotEmpty().WithMessage("QuestionId is required.");
        RuleFor(x => x.CreateReviewSessionRequest.IsCorrect).NotNull().WithMessage("IsCorrect is required.");

    }
}