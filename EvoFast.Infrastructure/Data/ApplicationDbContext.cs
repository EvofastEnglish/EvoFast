using System.Reflection;
using EvoFast.Application.Data;
using EvoFast.Domain.Models;

namespace EvoFast.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
    
    public DbSet<WordSet> WordSets => Set<WordSet>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Answer> Answers => Set<Answer>();
    public DbSet<WordSetAttempt> WordSetAttempts => Set<WordSetAttempt>();
    public DbSet<QuestionAttempt> QuestionAttempts => Set<QuestionAttempt>();

    public DbSet<User> Users => Set<User>();
}