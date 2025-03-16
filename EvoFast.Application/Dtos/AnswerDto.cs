namespace EvoFast.Application.Dtos;

public class AnswerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string TranslatedName { get; set; }
    public bool IsCorrect { get; set; }
}