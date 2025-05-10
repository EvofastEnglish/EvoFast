using EvoFast.Domain.Models;
using EvoFast.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvoFast.Infrastructure.Data.Configurations;

public class AiTestSectionResultConfiguration : IEntityTypeConfiguration<AiTestSectionResult>
{
    public void Configure(EntityTypeBuilder<AiTestSectionResult> builder)
    {
        builder.ToTable("AiTestSectionResult");
        builder.HasKey(x => x.Id);
        builder.HasIndex(a => new { a.AiTestSectionId, a.AiTestResultId }).IsUnique();
    }
}