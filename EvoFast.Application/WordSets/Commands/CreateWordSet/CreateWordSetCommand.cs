using EvoFast.Application.Dtos;

namespace EvoFast.Application.WordSets.Commands.CreateWordSet;

public record CreateWordSetCommand(WordSetDto WordSet) : ICommand<WordSetCreateResult>;
public record WordSetCreateResult(Guid Id);

public class CreateWordSetCommandValidator : AbstractValidator<CreateWordSetCommand>
{
    public CreateWordSetCommandValidator()
    {
        RuleFor(x => x.WordSet.WordSetName).NotEmpty().WithMessage("WordSetName is required.");
    }
}