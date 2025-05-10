using EvoFast.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.Data;

public interface IApplicationDbContext
{
    public DbSet<WordSet> WordSets { get;}
    public DbSet<Question> Questions { get;}
    public DbSet<Answer> Answers { get;}
    public DbSet<WordSetAttempt> WordSetAttempts { get;}
    public DbSet<QuestionAttempt> QuestionAttempts { get;}
    public DbSet<User> Users { get;}
    public DbSet<Category> Categories { get;}
    public DbSet<WordSetCategory> WordSetCategories { get;}
    public DbSet<Conversation> Conversations { get;}
    public DbSet<Message> Messages { get;}
    public DbSet<ReviewSession> ReviewSessions { get;}
    public DbSet<AiTest> AiTests { get;}
    public DbSet<AiTestSection> AiTestSections { get;}
    public DbSet<AiTestSectionQuestion> AiTestSectionQuestions { get;}
    public DbSet<AiTestResult> AiTestResults { get;}
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}