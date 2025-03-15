namespace EvoFast.Application.Categories.Command.AssignWordSet;

public record AssignWordSetCommand(AssignWordSetRequest AssignWordSetRequest) : ICommand<AssignWordSetResult>;

public record AssignWordSetRequest(Guid CategoryId, Guid WordSetId);

public record AssignWordSetResult(Guid Id);

public class AssignWordSetCommandValidator : AbstractValidator<AssignWordSetCommand>
{
    public AssignWordSetCommandValidator()
    {
        RuleFor(x => x.AssignWordSetRequest.WordSetId).NotEmpty().WithMessage("WordSetId is required.");
        RuleFor(x => x.AssignWordSetRequest.CategoryId).NotEmpty().WithMessage("CategoryId is required.");

    }
}