using EvoFast.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.Data;

public interface IApplicationDbContext
{
    public DbSet<WordSet> WordSets { get;}
    public DbSet<User> Users { get;}
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}