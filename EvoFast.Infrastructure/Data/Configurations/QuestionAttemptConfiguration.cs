using EvoFast.Domain.Models;
using EvoFast.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvoFast.Infrastructure.Data.Configurations;

public class QuestionAttemptConfiguration : IEntityTypeConfiguration<QuestionAttempt>
{
    public void Configure(EntityTypeBuilder<QuestionAttempt> builder)
    {
        builder.ToTable("QuestionAttempt");
        builder.HasKey(x => x.Id);
        builder.HasIndex(q => new { q.WordSetAttemptId, q.QuestionId }).IsUnique();
    }
}