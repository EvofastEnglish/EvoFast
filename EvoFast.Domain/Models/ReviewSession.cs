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
    public bool IsConfidence { get; private set; }
    public bool IsDeleted { get; private set; }
    
    public ReviewSession(Guid userId, Guid questionId, bool isCorrect)
    {
        UserId = userId;
        QuestionId = questionId;
        IsCorrect = isCorrect;
        IsDeleted = false;
        IsConfidence = false;
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
    
    private void SetConfidence(bool isConfidence)
    {
        IsConfidence = isConfidence;

        if (IsConfidence)
        {
            NextReviewDate = MistakeDate.AddDays(30);
        }
        else
        {
            ReviewStage = 0;
            NextReviewDate = MistakeDate.AddDays(1);
        }
    }
    
    private void UpdateReviewSession()
    {
        ReviewStage += 1;
        if (ReviewStage == 1)
        {
            NextReviewDate = NextReviewDate.AddDays(3);
        }else if (ReviewStage == 2)
        {
            NextReviewDate = NextReviewDate.AddDays(4);
        }else if (ReviewStage == 3)
        {
            NextReviewDate = NextReviewDate.AddDays(23);
        }else if (ReviewStage >= 4)
        {
            IsDeleted = true;
        }
    }

}