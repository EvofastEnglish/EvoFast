using BuildingBlocks.Exceptions;
using EvoFast.Application.Data;
using EvoFast.Domain.ValueObjects;

namespace EvoFast.Application.WordSets.Commands.DeleteWordSet;

public class DeleteWordSetHandler (IApplicationDbContext dbContext)
    : ICommandHandler<DeleteWordSetCommand, DeleteWordSetResult>
{
    public async Task<DeleteWordSetResult> Handle(DeleteWordSetCommand command, CancellationToken cancellationToken)
    {
        var wordSetId = WordSetId.Of(command.WordSetId);
        var wordSet = await dbContext.WordSets.FindAsync([wordSetId], cancellationToken);
        if (wordSet == null)
        {
            throw new NotFoundException("WordSet", command.WordSetId);
        }
        dbContext.WordSets.Remove(wordSet);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteWordSetResult(true);
    }
}