namespace EvoFast.Application.Dtos;

public record QuestionAttemptDto(
    bool IsBookmarked,
    Guid WordSetAttemptId,
    QuestionDto Question
    // WordSetAttemptDto WordSetAttempt
    );