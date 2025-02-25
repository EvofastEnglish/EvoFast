namespace EvoFast.Application.Dtos;

public record QuestionAttemptDto(
    bool IsBookmarked,
    QuestionDto Question
    // WordSetAttemptDto WordSetAttempt
    );