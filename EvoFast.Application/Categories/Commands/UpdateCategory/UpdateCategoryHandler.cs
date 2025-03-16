using EvoFast.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateCategoryCommand, UpdateCategoryResult>
{
    public async Task<UpdateCategoryResult> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories
            .FirstOrDefaultAsync(c => c.Id == command.Category.Id, cancellationToken);

        if (category == null)
        {
            throw new Exception("Category not found");
        }
        category.Update(command.Category.Name);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new UpdateCategoryResult(command.Category.Id);       }
}