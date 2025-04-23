using BuildingBlocks.Exceptions;
using EvoFast.Application.Data;

namespace EvoFast.Application.WordSetAttempts.Commands.CompleteWordSetAttempt;

public class CompleteWordSetAttemptHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CompleteWordSetAttemptCommand, CompleteWordSetAttemptResult>
{
    public async Task<CompleteWordSetAttemptResult> Handle(CompleteWordSetAttemptCommand command, CancellationToken cancellationToken)
    {
        var id = command.WordSetAttempt.Id;
        var wordSetAttempt = await dbContext.WordSetAttempts.FindAsync([id], cancellationToken);
        if (wordSetAttempt is null)
        {
            throw new NotFoundException("WordSetAttempt", id);
        }
        wordSetAttempt.CompletedAt = DateTime.UtcNow;
        dbContext.WordSetAttempts.Update(wordSetAttempt);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CompleteWordSetAttemptResult(wordSetAttempt.Id);    
    }
}