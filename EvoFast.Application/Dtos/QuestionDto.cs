namespace EvoFast.Application.Dtos;

public record QuestionDto(
    Guid Id,
    string Name,
    Guid? WordSetCategoryId,
    List<AnswerDto> Answers
    );