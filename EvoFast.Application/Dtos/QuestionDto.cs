namespace EvoFast.Application.Dtos;

public record QuestionDto(
    Guid Id,
    string Name,
    string TranslatedName
,    Guid? WordSetCategoryId,
    List<AnswerDto> Answers
    );