using EvoFast.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvoFast.Infrastructure.Data.Configurations;

public class ReviewSessionConfiguration : IEntityTypeConfiguration<ReviewSession>
{
    public void Configure(EntityTypeBuilder<ReviewSession> builder)
    {
        builder.ToTable("ReviewSession");
        builder.HasKey(x => x.Id);
        builder.HasIndex(q => new { q.UserId, q.QuestionId }).IsUnique();
    }
}