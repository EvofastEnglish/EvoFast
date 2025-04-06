using System.Net.Http.Headers;
using BuildingBlocks.Pagination;
using EvoFast.Application.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Newtonsoft.Json;

namespace EvoFast.Tests.IntegrationTests.Controllers;

public class ConversationsController : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ConversationsController(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task GetConversations_ReturnsPaginatedResult()
    {
        // Arrange
        // Act
        var response = await _client.GetAsync($"Conversations");
        var responseContent = await response.Content.ReadAsStringAsync();
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        responseContent.Contains("conversations").Should().BeTrue();
    }
}