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
        builder.Property(x => x.Id).HasConversion(
            wordSetId => wordSetId.Value, 
            dbId => WordSetId.Of(dbId));
        builder.ComplexProperty(x => x.WordSetName, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value)
                .HasColumnName(nameof(WordSet.WordSetName))
                .HasMaxLength(100)
                .IsRequired();
        });
    }
}