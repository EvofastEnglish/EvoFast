using EvoFast.Application.Conversations.Commands.AddConversation;
using EvoFast.Application.Dtos;
using EvoFast.Application.Services;
using EvoFast.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

namespace EvoFast.Tests.IntegrationTests.Handlers.Conversations.Commands;

public class MockCreateConversationHandler
{
    private readonly CreateConversationHandler _handler;
    private readonly ApplicationDbContext _dbContext;
    public MockCreateConversationHandler()
    {
        DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        Mock<IChatGptService> mockChatGptService = new();
        mockChatGptService
            .Setup(service => service.GetChatGptResponseAsync(It.IsAny<List<ChatGptMessageDto>>()))
            .ReturnsAsync(("assistant", "This is a mock response"));

        var languageOptions = new Dictionary<string, string> { { "en", "English" } };
        var options = Options.Create(languageOptions);

        _dbContext = new ApplicationDbContext(dbContextOptions);
        _handler = new CreateConversationHandler(_dbContext, mockChatGptService.Object, options);
    }
    
    [Fact]
    public async Task Handle_ShouldCreateConversationAndMessages_WhenValidCommand()
    {
        // Arrange
        var command = new CreateConversationCommand(new CreateConversationRequest("en", "Student", "Teacher", "Improve your speaking English", Guid.NewGuid()));
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        var conversation = _dbContext.Conversations.Local.FirstOrDefault(c => c.Id == result.Id);
        Assert.NotNull(conversation);
        Assert.Equal("Improve your speaking English", conversation.Topic);
        var systemMessage = _dbContext.Messages.Local.FirstOrDefault(m => m.Role == "system");
        Assert.NotNull(systemMessage);
    }
}