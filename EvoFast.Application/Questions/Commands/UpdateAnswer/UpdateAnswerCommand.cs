namespace EvoFast.Application.Questions.Commands.UpdateAnswer;

public record UpdateAnswerCommand(UpdateAnswerRequest Answer) : ICommand<UpdateAnswerResult>;

public record UpdateAnswerRequest(
    Guid QuestionId,
    Guid Id,
    String? Name,
    String? TranslatedName,
    Boolean? IsCorrect);

public record UpdateAnswerResult(Guid Id);

public class UpdateAnswerCommandValidator : AbstractValidator<UpdateAnswerCommand>
{
    public UpdateAnswerCommandValidator()
    {
    }
}