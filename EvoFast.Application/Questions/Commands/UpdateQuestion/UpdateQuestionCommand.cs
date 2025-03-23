namespace EvoFast.Application.Questions.Commands.UpdateQuestion;

public record UpdateQuestionCommand(UpdateQuestionRequest Question) : ICommand<UpdateQuestionResult>;

public record UpdateQuestionRequest(Guid Id, String? Name, String? TranslatedName);
public record UpdateQuestionResult(Guid Id);

public class UpdateQuestionCommandValidator : AbstractValidator<UpdateQuestionCommand>
{
    public UpdateQuestionCommandValidator()
    {
        RuleFor(x => x.Question.Id).NotEmpty().WithMessage("Id is required.");
    }
}