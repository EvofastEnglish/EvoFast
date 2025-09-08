using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class ReviewSession : Entity<Guid>
{
    [ForeignKey("UserId")]
    public Guid UserId { get; private set; }
    public virtual User User { get; set; }
    
    [ForeignKey("QuestionId")]
    public Guid QuestionId { get; private set; }
    public virtual Question Question { get; set; }
    
    public bool IsCorrect { get; private set; }
    public DateTime MistakeDate { get; private set; }
    public DateTime NextReviewDate { get; private set; }
    public int ReviewStage { get; private set; }
    public bool? IsConfidence { get; private set; }
    public bool IsDeleted { get; private set; }
    
    public ReviewSession(Guid userId, Guid questionId, bool isCorrect)
    {
        UserId = userId;
        QuestionId = questionId;
        IsCorrect = isCorrect;
        IsDeleted = false;
        IsConfidence = null;
        if (!IsCorrect)
        {
            ReviewStage = 0;
            MistakeDate = DateTime.UtcNow;
            NextReviewDate = MistakeDate.AddDays(1);
        }
        else
        {
            ReviewStage = 0;
            MistakeDate = DateTime.UtcNow;
            NextReviewDate = MistakeDate.AddDays(30);
        }
    }
    
    public void SetConfidence(bool isConfidence)
    {
        IsConfidence = isConfidence;

        if (IsConfidence == true)
        {
            ReviewStage = 3;
            NextReviewDate = MistakeDate.AddDays(30);
        }
        else
        {
            ReviewStage = 0;
            NextReviewDate = DateTime.UtcNow.AddDays(1);
        }
    }
    
    public void UpdateReviewSession()
    {
        ReviewStage += 1;
        if (ReviewStage == 1)
        {
            NextReviewDate = DateTime.UtcNow.AddDays(3);
        }else if (ReviewStage == 2)
        {
            NextReviewDate = DateTime.UtcNow.AddDays(4);
        }else if (ReviewStage == 3)
        {
            NextReviewDate = DateTime.UtcNow.AddDays(23);
        }else if (ReviewStage >= 4)
        {
            IsDeleted = true;
        }
    }

}