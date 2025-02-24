using EvoFast.Domain.Models;
using EvoFast.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvoFast.Infrastructure.Data.Configurations;

public class WordSetConfiguration : IEntityTypeConfiguration<WordSet>
{
    public void Configure(EntityTypeBuilder<WordSet> builder)
    {
        builder.ToTable("WordSet");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.NumberId).IsUnique();
    }
}