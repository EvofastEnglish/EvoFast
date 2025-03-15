using EvoFast.Domain.Models;
using EvoFast.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvoFast.Infrastructure.Data.Configurations;

public class WordSetCategoryConfiguration : IEntityTypeConfiguration<WordSetCategory>
{
    public void Configure(EntityTypeBuilder<WordSetCategory> builder)
    {
        builder.ToTable("WordSetCategory");
        builder.HasKey(x => x.Id);
        builder.HasIndex(q => new { q.WordSetId, q.CategoryId }).IsUnique();
    }
}