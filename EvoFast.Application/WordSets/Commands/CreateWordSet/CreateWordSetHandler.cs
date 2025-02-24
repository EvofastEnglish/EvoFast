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
            Guid.NewGuid(), 
            command.WordSet.NumberId);
        dbContext.WordSets.Add(wordSet);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateWordSetResult(wordSet.Id);
    }
}