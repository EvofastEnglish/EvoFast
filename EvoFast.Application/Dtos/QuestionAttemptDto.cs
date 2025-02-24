using EvoFast.Domain.Models;

namespace EvoFast.Application.Dtos;

public record QuestionAttemptDto(
    bool IsBookmarked,
    QuestionDto Question,
    WordSetAttempt WordSetAttempt
    );