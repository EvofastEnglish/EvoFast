namespace EvoFast.Application.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(UpdateCategoryRequest Category) : ICommand<UpdateCategoryResult>;

public record UpdateCategoryRequest(Guid Id, String Name);
public record UpdateCategoryResult(Guid Id);

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Category.Id).NotEmpty().WithMessage("CategoryId is required.");
        RuleFor(x => x.Category.Name).NotEmpty().WithMessage("Name is required.");
    }
}