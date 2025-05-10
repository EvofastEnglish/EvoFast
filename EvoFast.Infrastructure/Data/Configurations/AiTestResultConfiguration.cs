using EvoFast.Domain.Models;
using EvoFast.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvoFast.Infrastructure.Data.Configurations;

public class AiTestResultConfiguration : IEntityTypeConfiguration<AiTestResult>
{
    public void Configure(EntityTypeBuilder<AiTestResult> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(a => new { a.UserId, a.AiTestId }).IsUnique();
    }
}