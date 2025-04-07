namespace EvoFast.Tests.Extensions.Provider;

public interface ITestUserProvider
{
    Guid UserId { get; set; }
}

public class TestUserProvider : ITestUserProvider
{
    public Guid UserId { get; set; } = Guid.NewGuid();

}