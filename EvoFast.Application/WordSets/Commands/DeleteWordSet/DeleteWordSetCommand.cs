namespace EvoFast.Application.WordSets.Commands.DeleteWordSet;

public record DeleteWordSetCommand(Guid WordSetId) : ICommand<DeleteWordSetResult>;
public record DeleteWordSetResult(bool IsSuccess);