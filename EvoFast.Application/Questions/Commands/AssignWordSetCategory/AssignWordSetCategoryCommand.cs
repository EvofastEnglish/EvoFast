namespace EvoFast.Application.Questions.Commands.AssignWordSetCategory;

public record AssignWordSetCategoryCommand(AssignWordSetCategoryRequest AssignWordSetCategoryRequest) : ICommand<AssignWordSetCategoryResult>;

public record AssignWordSetCategoryRequest(Guid QuestionId, Guid WordSetCategoryId);
public record AssignWordSetCategoryResult(Guid Id);

public class AssignWordSetCategoryCommandValidator : AbstractValidator<AssignWordSetCategoryResult>
{
    public AssignWordSetCategoryCommandValidator()
    {
    }
}