using System.ComponentModel.DataAnnotations.Schema;
using EvoFast.Domain.Abstractions;

namespace EvoFast.Domain.Models;

public class Conversation : Entity<Guid>
{
    public string YourRole { get; set; }
    public string AIRole { get; set; }
    public string Topic { get; set; }
    [ForeignKey("UserId")]
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
}