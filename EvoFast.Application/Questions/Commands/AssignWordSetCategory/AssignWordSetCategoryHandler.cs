using EvoFast.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.Questions.Commands.AssignWordSetCategory;

public class AssignWordSetCategoryHandler(IApplicationDbContext dbContext)
    : ICommandHandler<AssignWordSetCategoryCommand, AssignWordSetCategoryResult>
{
    public async Task<AssignWordSetCategoryResult> Handle(AssignWordSetCategoryCommand command, CancellationToken cancellationToken)
    {
        var question = await dbContext.Questions
            .FirstOrDefaultAsync(q => q.Id == command.AssignWordSetCategoryRequest.QuestionId, cancellationToken);
        
        if (question == null)
        {
            throw new Exception("Question not found");
        }
        
        question.WordSetCategoryId = command.AssignWordSetCategoryRequest.WordSetCategoryId;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new AssignWordSetCategoryResult(question.Id);
    }
}