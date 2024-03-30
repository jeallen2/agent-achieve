using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Features.Clients;
using FluentAssertions;
using AgentAchieve.UnitTests.Common;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

/// <summary>
/// This class contains unit tests for the ClientService class.
/// </summary>
namespace AgentAchieve.Infrastructure.UnitTests.Services;
public class ClientServiceTests(ITestOutputHelper outputHelper, DatabaseFixture dbFixture) : TestBase<ClientService>(outputHelper, dbFixture)
{
    [Trait("Description", "Verifies that all clients are returned")]
    [Fact]
    public async Task GetAllDtoAsync_ShouldReturnAllClients()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating ClientService and adding clients");
        var clientService = CreateClientService();
        var client1 = new Client("John", "Doe", "1234567890") { Email = "john.doe@example.com" };
        var client2 = new Client("Jane", "Doe", "0987654321") { Email = "jane.doe@example.com" };

        await AddAsync(client1);
        await AddAsync(client2);

        // Act
        Logger.LogInformation("Calling GetAllDtoAsync");
        var result = await clientService.GetAllDtoAsync();

        // Assert
        Logger.LogInformation("Asserting that all clients are returned");
        result.Should().HaveCount(2);
        Logger.LogInformation("All clients returned successfully");
    }

    [Trait("Description", "Verifies that the correct client is returned by ID")]
    [Fact]
    public async Task GetDtoByIdAsync_ShouldReturnCorrectClient()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating ClientService and adding a client");
        var clientService = CreateClientService();
        var client = new Client("John", "Doe", "1234567890") { Email = "john.doe@example.com" };
        await AddAsync(client);

        // Act
        Logger.LogInformation("Calling GetDtoByIdAsync");
        var result = await clientService.GetDtoByIdAsync(client.Id);

        // Assert
        Logger.LogInformation("Asserting that the correct client is returned");
        result.Should().NotBeNull();
        result!.Id.Should().Be(client.Id);
        result.FirstName.Should().Be(client.FirstName);
        result.LastName.Should().Be(client.LastName);
        result.PhoneNumber.Should().Be(client.PhoneNumber);
        result.Email.Should().Be(client.Email);
        Logger.LogInformation("Correct client returned successfully");
    }

    [Trait("Description", "Verifies that a client is added to the database")]
    [Fact]
    public async Task CreateClientAsync_ShouldAddClientToDatabase()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating ClientService and a client DTO");
        var clientService = CreateClientService();
        var clientDto = new ClientDto
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            Email = "john.doe@example.com",
            Street = "123 Main St",
            City = "Anytown",
            State = "CA",
            ZipCode = "12345",
            Country = "USA",
            Birthdate = new DateTime(1980, 1, 1),
            Occupation = "Software Developer",
            Employer = "Acme Corp",
            ReferredBy = "Jane Smith",
            Notes = "Test client"
        };

        // Act
        Logger.LogInformation("Calling CreateClientAsync");
        var result = await clientService.CreateClientAsync(clientDto);

        // Assert
        Logger.LogInformation("Asserting that the client is added to the database");
        result.Should().NotBeNull();
        var dbClient = await FindAsync<Client>(result.Id);
        dbClient.Should().NotBeNull();
        dbClient!.FirstName.Should().Be(clientDto.FirstName);
        dbClient.LastName.Should().Be(clientDto.LastName);
        dbClient.PhoneNumber.Should().Be(clientDto.PhoneNumber);
        dbClient.Email.Should().Be(clientDto.Email);
        dbClient.Street.Should().Be(clientDto.Street);
        dbClient.City.Should().Be(clientDto.City);
        dbClient.State.Should().Be(clientDto.State);
        dbClient.ZipCode.Should().Be(clientDto.ZipCode);
        dbClient.Country.Should().Be(clientDto.Country);
        dbClient.Birthdate.Should().Be(clientDto.Birthdate);
        dbClient.Occupation.Should().Be(clientDto.Occupation);
        dbClient.Employer.Should().Be(clientDto.Employer);
        dbClient.ReferredBy.Should().Be(clientDto.ReferredBy);
        dbClient.Notes.Should().Be(clientDto.Notes);
        Logger.LogInformation("Client added successfully");
    }

    [Trait("Description", "Verifies that a client is updated in the database")]
    [Fact]
    public async Task UpdateClientAsync_ShouldUpdateClientInDatabase()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating ClientService, adding a client, and creating a client DTO");
        var clientService = CreateClientService();
        var client = new Client("John", "Doe", "1234567890") { Email = "john.doe@example.com" };
        await AddAsync(client);
        var clientDto = new ClientDto { Id = client.Id, FirstName = "Jane", LastName = "Doe", PhoneNumber = "0987654321", Email = "jane.doe@example.com" };

        // Act
        Logger.LogInformation("Calling UpdateClientAsync");
        var result = await clientService.UpdateClientAsync(clientDto);

        // Assert
        Logger.LogInformation("Asserting that the client is updated in the database");
        result.Should().NotBeNull();
        var dbClient = await FindAsync<Client>(result.Id);
        dbClient.Should().NotBeNull();
        dbClient!.FirstName.Should().Be(clientDto.FirstName);
        dbClient.LastName.Should().Be(clientDto.LastName);
        dbClient.PhoneNumber.Should().Be(clientDto.PhoneNumber);
        dbClient.Email.Should().Be(clientDto.Email);
        Logger.LogInformation("Client updated successfully");
    }

    [Trait("Description", "Verifies that a client is removed from the database")]
    [Fact]
    public async Task DeleteClientAsync_ShouldRemoveClientFromDatabase()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating ClientService and adding a client");
        var clientService = CreateClientService();
        var client = new Client("John", "Doe", "1234567890") { Email = "john.doe@example.com" };
        await AddAsync(client);

        // Act
        Logger.LogInformation("Calling DeleteClientAsync");
        await clientService.DeleteClientAsync(client.Id);

        // Assert
        Logger.LogInformation("Asserting that the client is removed from the database");
        var dbClient = await FindAsync<Client>(client.Id);
        dbClient.Should().BeNull();
        Logger.LogInformation("Client removed successfully");
    }
}