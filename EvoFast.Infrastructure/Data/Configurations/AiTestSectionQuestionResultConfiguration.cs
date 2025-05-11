using EvoFast.Domain.Models;
using EvoFast.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvoFast.Infrastructure.Data.Configurations;

public class AiTestSectionQuestionResultConfiguration : IEntityTypeConfiguration<AiTestSectionQuestionResult>
{
    public void Configure(EntityTypeBuilder<AiTestSectionQuestionResult> builder)
    {
        builder.ToTable("AiTestSectionQuestionResult");
        builder.HasKey(x => x.Id);
        builder.HasIndex(a => new { a.AiTestSectionQuestionId, a.AiTestSectionResultId }).IsUnique();
    }
}