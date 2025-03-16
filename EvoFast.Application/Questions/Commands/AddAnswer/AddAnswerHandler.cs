using EvoFast.Application.Data;
using EvoFast.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.Questions.Commands.AddAnswer;

public class AddAnswerHandler(IApplicationDbContext dbContext)
    : ICommandHandler<AddAnswerCommand, AddAnswerResult>
{
    public async Task<AddAnswerResult> Handle(AddAnswerCommand command, CancellationToken cancellationToken)
    {
        var question = await dbContext.Questions
            .Include(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == command.Answer.QuestionId, cancellationToken);
        
        if (question == null)
        {
            throw new Exception("Question not found");
        }
        question.AddAnswer(command.Answer.Name, command.Answer.TranslatedName, command.Answer.IsCorrect);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new AddAnswerResult(question.Id);
    }
}