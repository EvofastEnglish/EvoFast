namespace EvoFast.Application.Dtos;

public record QuestionDto(
    string Name,
    List<AnswerDto> Answers
    );