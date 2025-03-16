using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class Question : Aggregate<Guid>
{
    public string Name { get; set; }
    
    [ForeignKey("WordSetId")]
    public Guid WordSetId { get; set; }
    public virtual WordSet WordSet { get; set; }
    
    private readonly List<Answer> _answers = new();
    public IReadOnlyList<Answer> Answers => _answers.AsReadOnly();
    
    public void AddAnswer(String name, String translatedName, Boolean isCorrect)
    {
        var newAnswer = new Answer
        {
            Id = Guid.NewGuid(),
            Name = name,
            TranslatedName = translatedName,
            IsCorrect = isCorrect,
            QuestionId = Id
        };
        _answers.Add(newAnswer);
    }
    
    public void UpdateAnswer(Guid answerId, string? newName, string? newTranslatedName, bool? isCorrect)
    {
        var answer = _answers.FirstOrDefault(a => a.Id == answerId);
        if (answer == null)
        {
            throw new Exception("Answer not found");
        }
        answer.Update(newName, newTranslatedName, isCorrect);
    }
}