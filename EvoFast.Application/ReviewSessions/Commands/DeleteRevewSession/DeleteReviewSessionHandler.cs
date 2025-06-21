using EvoFast.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.ReviewSessions.Commands.DeleteRevewSession;

public class DeleteReviewSessionHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteReviewSessionCommand, DeleteReviewSessionResult>
{
    public async Task<DeleteReviewSessionResult> Handle(DeleteReviewSessionCommand command,
        CancellationToken cancellationToken)
    {
        var reviewSession = await dbContext.ReviewSessions
            .FirstOrDefaultAsync(q => q.Id == command.Id, cancellationToken);

        if (reviewSession == null)
        {
            throw new Exception("Review session not found");
        }

        dbContext.ReviewSessions.Remove(reviewSession);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteReviewSessionResult(true);
    }
}