using EvoFast.Domain.Models;
using EvoFast.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvoFast.Infrastructure.Data.Configurations;

public class WordSetAttemptConfiguration : IEntityTypeConfiguration<WordSetAttempt>
{
    public void Configure(EntityTypeBuilder<WordSetAttempt> builder)
    {
        builder.ToTable("WordSetAttempt");
        builder.HasKey(x => x.Id);
        builder.HasIndex(w => new { w.UserId, w.WordSetId }).IsUnique();
    }
}