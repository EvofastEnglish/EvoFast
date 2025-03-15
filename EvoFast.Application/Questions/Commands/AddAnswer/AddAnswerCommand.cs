namespace EvoFast.Application.Questions.Commands.AddAnswer;

public record AddAnswerCommand(AddAnswerRequest Answer) : ICommand<AddAnswerResult>;

public record AddAnswerRequest(
    String Name,
    String TranslatedName,
    Boolean IsCorrect,
    Guid QuestionId);

public record AddAnswerResult(Guid Id);

public class AddAnswerCommandValidator : AbstractValidator<AddAnswerCommand>
{
    public AddAnswerCommandValidator()
    {
    }
}