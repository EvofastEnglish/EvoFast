namespace EvoFast.Application.Dtos;

public record QuestionAttemptDto(
    bool IsBookmarked,
    Guid WordSetAttemptId,
    Guid Id,
    QuestionDto Question
    );