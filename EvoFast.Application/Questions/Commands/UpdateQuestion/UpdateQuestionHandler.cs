using EvoFast.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.Questions.Commands.UpdateQuestion;

public class UpdateQuestionHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateQuestionCommand, UpdateQuestionResult>
{
    public async Task<UpdateQuestionResult> Handle(UpdateQuestionCommand command, CancellationToken cancellationToken)
    {
        var question = await dbContext.Questions
            .FirstOrDefaultAsync(q => q.Id == command.Question.Id, cancellationToken);

        if (question == null)
        {
            throw new Exception("Question not found");
        }

        question.Update(command.Question.Name, command.Question.TranslatedName);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new UpdateQuestionResult(command.Question.Id);       
    }
}