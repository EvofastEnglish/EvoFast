using EvoFast.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.Data;

public interface IApplicationDbContext
{
    public DbSet<WordSet> WordSets { get;}
    public DbSet<Question> Questions { get;}
    public DbSet<Answer> Answers { get;}
    public DbSet<User> Users { get;}
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}