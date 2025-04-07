using System.Net.Http.Headers;
using System.Text;
using BuildingBlocks.Pagination;
using EvoFast.Application.Conversations.Commands.AddConversation;
using EvoFast.Application.Conversations.Queries.GetConversations;
using EvoFast.Application.Dtos;
using EvoFast.Domain.Models;
using EvoFast.Infrastructure.Data;
using EvoFast.Tests.Extensions.Provider;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace EvoFast.Tests.IntegrationTests.Controllers;

public class ConversationsController : IClassFixture<CustomWebApplicationFactory<Program>>
{

    private readonly CustomWebApplicationFactory<Program> _factory;

    public ConversationsController(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task GetConversations_ReturnsPaginatedResult()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var client = _factory.CreateClient();
        var userProvider = scope.ServiceProvider.GetRequiredService<ITestUserProvider>();

        var user = new User
        {
            Id = userProvider.UserId,
            Email = "test.conversation@email.com",
            Username = "test.conversation",
        };
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        var conversationRequest = new CreateConversationRequest("en", "Student", "Teacher", "Improve your speaking English", user.Id);
        var jsonContent = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(conversationRequest),
            Encoding.UTF8,
            "application/json"
            );
        await client.PostAsync($"Conversations", jsonContent);
        // Act
        var response = await client.GetAsync($"Conversations");
        var responseContent = await response.Content.ReadAsStringAsync();
        var conversationsResult = JsonConvert.DeserializeObject<GetConversationsResult>(responseContent);
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        conversationsResult.Conversations.Count.Should().BeGreaterThan(0);
    }
}