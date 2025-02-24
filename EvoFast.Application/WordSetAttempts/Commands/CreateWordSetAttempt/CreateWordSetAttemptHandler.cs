using EvoFast.Application.Data;
using EvoFast.Domain.Models;

namespace EvoFast.Application.WordSetAttempts.Commands.CreateWordSetAttempt;

public class CreateWordSetAttemptHandler 
    (IApplicationDbContext dbContext)
    : ICommandHandler<CreateWordSetAttemptCommand, CreateWordSetAttemptResult>
{
    public async Task<CreateWordSetAttemptResult> Handle(CreateWordSetAttemptCommand command, CancellationToken cancellationToken)
    {
        var wordSetAttempt = dbContext.WordSetAttempts
            .FirstOrDefault(ws => ws.WordSetId == command.WordSetAttempt.WordSetId && ws.UserId == command.WordSetAttempt.UserId);
        if (wordSetAttempt == null)
        {
            wordSetAttempt = new WordSetAttempt
            {
                WordSetId = command.WordSetAttempt.WordSetId,
                UserId = command.WordSetAttempt.UserId,
            };
            dbContext.WordSetAttempts.Add(wordSetAttempt);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        return new CreateWordSetAttemptResult(wordSetAttempt.Id);
    }
}