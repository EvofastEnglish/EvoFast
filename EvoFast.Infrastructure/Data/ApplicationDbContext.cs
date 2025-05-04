using System.Reflection;
using System.Security.Claims;
using EvoFast.Application.Data;
using EvoFast.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace EvoFast.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly Guid? _currentUserId;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) 
        : base(options)
    {
        _currentUserId = GetCurrentUserId(httpContextAccessor);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
        builder.Entity<Conversation>()
            .HasQueryFilter(c => c.UserId == _currentUserId);
        builder.Entity<WordSetAttempt>()
            .HasQueryFilter(c => c.UserId == _currentUserId);
        builder.Entity<ReviewSession>()
            .HasQueryFilter(c => c.UserId == _currentUserId && c.NextReviewDate <= DateTime.UtcNow);
    }
    
    private Guid GetCurrentUserId(IHttpContextAccessor httpContextAccessor)
    {
        var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return userIdClaim != null ? Guid.Parse(userIdClaim) : Guid.Empty;
    }
    
    public DbSet<WordSet> WordSets => Set<WordSet>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Answer> Answers => Set<Answer>();
    public DbSet<WordSetAttempt> WordSetAttempts => Set<WordSetAttempt>();
    public DbSet<QuestionAttempt> QuestionAttempts => Set<QuestionAttempt>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<WordSetCategory> WordSetCategories => Set<WordSetCategory>();
    public DbSet<Conversation> Conversations => Set<Conversation>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<ReviewSession> ReviewSessions => Set<ReviewSession>();

}