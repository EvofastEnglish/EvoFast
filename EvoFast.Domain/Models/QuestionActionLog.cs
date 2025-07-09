using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class QuestionActionLogs : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid QuestionId { get; set; }
    public bool IsCorrect { get; set; }
}