using EvoFast.Application.Data;
using EvoFast.Domain.Models;

namespace EvoFast.Application.Categories.Command.AssignWordSet;

public class AssignWordSetHandler(IApplicationDbContext dbContext)
    : ICommandHandler<AssignWordSetCommand, AssignWordSetResult>
{
    public async Task<AssignWordSetResult> Handle(AssignWordSetCommand command, CancellationToken cancellationToken)
    {
        var wordSetCategory = dbContext.WordSetCategories
            .FirstOrDefault(ws => ws.WordSetId == command.AssignWordSetRequest.WordSetId && ws.CategoryId == command.AssignWordSetRequest.CategoryId);
        if (wordSetCategory == null)
        {
            wordSetCategory = new WordSetCategory
            {
                WordSetId = command.AssignWordSetRequest.WordSetId,
                CategoryId = command.AssignWordSetRequest.CategoryId,
            };
            dbContext.WordSetCategories.Add(wordSetCategory);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        return new AssignWordSetResult(wordSetCategory.Id);    }
}