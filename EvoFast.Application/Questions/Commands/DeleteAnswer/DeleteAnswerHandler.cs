using EvoFast.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.Questions.Commands.DeleteAnswer;

public class DeleteAnswerHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteAnswerCommand, DeleteAnswerResult>
{
    public async Task<DeleteAnswerResult> Handle(DeleteAnswerCommand command, CancellationToken cancellationToken)
    {
        var question = await dbContext.Questions
            .Include(q => q.Answers) 
            .FirstOrDefaultAsync(q => q.Id == command.QuestionId, cancellationToken);

        if (question == null)
        {
            throw new Exception("Question not found");
        }

        question.RemoveAnswer(command.AnswerId);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteAnswerResult(true);    
    }
}