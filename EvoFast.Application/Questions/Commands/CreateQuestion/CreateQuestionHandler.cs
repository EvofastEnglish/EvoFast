using EvoFast.Application.Data;
using EvoFast.Domain.Models;

namespace EvoFast.Application.Questions.Commands.CreateQuestion;

public class CreateQuestionHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateQuestionCommand, CreateQuestionResult>
{
    public async Task<CreateQuestionResult> Handle(CreateQuestionCommand command, CancellationToken cancellationToken)
    {
        var question = new Question
        {
            WordSetId = command.Question.WordSetId,
            Name = command.Question.Name
        };
        dbContext.Questions.Add(question);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateQuestionResult(question.Id);    }
}