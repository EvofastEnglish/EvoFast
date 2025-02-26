namespace EvoFast.Application.Dtos;

public record QuestionDto(
    Guid Id,
    string Name,
    List<AnswerDto> Answers
    );