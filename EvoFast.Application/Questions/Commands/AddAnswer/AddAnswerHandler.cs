using EvoFast.Application.Data;
using EvoFast.Domain.Models;
using Mapster;

namespace EvoFast.Application.Questions.Commands.AddAnswer;

public class AddAnswerHandler(IApplicationDbContext dbContext)
    : ICommandHandler<AddAnswerCommand, AddAnswerResult>
{
    public async Task<AddAnswerResult> Handle(AddAnswerCommand command, CancellationToken cancellationToken)
    {
        var answer = command.Answer.Adapt<Answer>();
        dbContext.Answers.Add(answer);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new AddAnswerResult(answer.Id);
    }
}