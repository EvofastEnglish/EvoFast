namespace EvoFast.Application.Dtos;

public record AiTestResultDto(
    Guid Id,
    Guid AiTestId,
    Guid UserId,
    string Evaluation
    );