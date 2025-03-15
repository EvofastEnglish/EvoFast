namespace EvoFast.Application.Questions.Commands.CreateQuestion;

public record CreateQuestionCommand(CreateQuestionRequest Question) : ICommand<CreateQuestionResult>;

public record CreateQuestionRequest(Guid WordSetId, String Name);

public record CreateQuestionResult(Guid Id);

public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionCommandValidator()
    {
    }
}