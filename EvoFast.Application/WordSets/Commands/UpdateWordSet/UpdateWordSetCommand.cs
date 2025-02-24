namespace EvoFast.Application.WordSets.Commands.UpdateWordSet;

public record UpdateWordSetCommand(UpdateWordSetRequest WordSet) : ICommand<UpdateWordSetResult>;

public record UpdateWordSetRequest(Guid Id, long NumberId);
public record UpdateWordSetResult(Guid Id);

public class UpdateWordSetCommandValidator : AbstractValidator<UpdateWordSetCommand>
{
    public UpdateWordSetCommandValidator()
    {
        RuleFor(x => x.WordSet.NumberId).NotEmpty().WithMessage("NumberId is required.");
    }
}
