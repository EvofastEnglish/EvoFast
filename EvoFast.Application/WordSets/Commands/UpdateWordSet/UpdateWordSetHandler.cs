using BuildingBlocks.Exceptions;
using EvoFast.Application.Data;
using EvoFast.Domain.ValueObjects;

namespace EvoFast.Application.WordSets.Commands.UpdateWordSet;

public class UpdateWordSetHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateWordSetCommand, UpdateWordSetResult>
{
    public async Task<UpdateWordSetResult> Handle(UpdateWordSetCommand command, CancellationToken cancellationToken)
    {
        var wordSetId = command.WordSet.Id;
        var wordSet = await dbContext.WordSets.FindAsync([wordSetId], cancellationToken);
        if (wordSet is null)
        {
            throw new NotFoundException("WordSet", wordSetId);
        }
        wordSet.Update(command.WordSet.NumberId);
        dbContext.WordSets.Update(wordSet);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new UpdateWordSetResult(wordSet.Id);
    }
}