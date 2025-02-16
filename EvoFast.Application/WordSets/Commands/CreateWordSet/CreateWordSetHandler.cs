using BuildingBlocks.CQRS;
using EvoFast.Application.Data;
 using EvoFast.Domain.Models;
using EvoFast.Domain.ValueObjects;

namespace EvoFast.Application.WordSets.Commands.CreateWordSet;

public class CreateWordSetHandler 
    (IApplicationDbContext dbContext)
    : ICommandHandler<CreateWordSetCommand, CreateWordSetResult>
{
    public async Task<CreateWordSetResult> Handle(CreateWordSetCommand command, CancellationToken cancellationToken)
    {
        var wordSet = WordSet.Create(
            WordSetId.Of(Guid.NewGuid()), 
            WordSetName.Of(command.WordSet.WordSetName), 
            command.WordSet.Description);
        dbContext.WordSets.Add(wordSet);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateWordSetResult(wordSet.Id.Value);
    }
}