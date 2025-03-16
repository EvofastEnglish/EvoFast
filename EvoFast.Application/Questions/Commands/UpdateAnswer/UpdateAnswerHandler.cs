using EvoFast.Application.Data;
using EvoFast.Application.Questions.Commands.AddAnswer;
using EvoFast.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.Questions.Commands.UpdateAnswer;

public class UpdateAnswerHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateAnswerCommand, UpdateAnswerResult>
{
    public async Task<UpdateAnswerResult> Handle(UpdateAnswerCommand command, CancellationToken cancellationToken)
    {
        var question = await dbContext.Questions
            .Include(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == command.Answer.QuestionId, cancellationToken);

        if (question == null)
        {
            throw new Exception("Question not found");
        }

        question.UpdateAnswer(command.Answer.Id, command.Answer.Name, command.Answer.TranslatedName, command.Answer.IsCorrect);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new UpdateAnswerResult(command.Answer.Id);    
    }
}