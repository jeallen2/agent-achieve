using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Features.SalesGoals;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace AgentAchieve.Infrastructure.UnitTests;
public class SalesGoalServiceTests(ITestOutputHelper outputHelper, DatabaseFixture dbFixture) : TestBase<SalesGoalService>(outputHelper, dbFixture)
{
    [Trait("Description", "Verifies that all sales goals are returned")]
    [Fact]
    public async Task GetAllDtoAsync_ShouldReturnAllSalesGoals()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SalesGoalService and adding sales goals");
        var salesGoalService = CreateSalesGoalService();
        var salesGoal1 = new SalesGoal("owner1") { GoalDate = new DateTime(2022, 1, 1), GoalAmount = 100000 };
        var salesGoal2 = new SalesGoal("owner2") { GoalDate = new DateTime(2022, 2, 1), GoalAmount = 200000 };

        await AddAsync(salesGoal1);
        await AddAsync(salesGoal2);

        // Act
        Logger.LogInformation("Calling GetAllDtoAsync");
        var result = await salesGoalService.GetAllDtoAsync();

        // Assert
        Logger.LogInformation("Asserting that all sales goals are returned");
        result.Should().HaveCount(2);
        Logger.LogInformation("All sales goals returned successfully");
    }

    [Trait("Description", "Verifies that the correct sales goal is returned by ID")]
    [Fact]
    public async Task GetDtoByIdAsync_ShouldReturnCorrectSalesGoal()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SalesGoalService and adding a sales goal");
        var salesGoalService = CreateSalesGoalService();
        var salesGoal = new SalesGoal("owner1") { GoalDate = new DateTime(2022, 1, 1), GoalAmount = 100000 };
        await AddAsync(salesGoal);

        // Act
        Logger.LogInformation("Calling GetDtoByIdAsync");
        var result = await salesGoalService.GetDtoByIdAsync(salesGoal.Id);

        // Assert
        Logger.LogInformation("Asserting that the correct sales goal is returned");
        result.Should().NotBeNull();
        result!.Id.Should().Be(salesGoal.Id);
        result.OwnedById.Should().Be(salesGoal.OwnedById);
        result.GoalDate.Should().Be(salesGoal.GoalDate);
        result.GoalAmount.Should().Be(salesGoal.GoalAmount);
        Logger.LogInformation("Correct sales goal returned successfully");
    }

    [Trait("Description", "Verifies that a sales goal is added to the database")]
    [Fact]
    public async Task CreateSalesGoalAsync_ShouldAddSalesGoalToDatabase()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SalesGoalService and a sales goal DTO");
        var salesGoalService = CreateSalesGoalService();
        var salesGoalDto = new SalesGoalDto
        {
            OwnedById = "Agent1",
            GoalDate = new DateTime(2022, 1, 1),
            GoalAmount = 100000
        };

        // Act
        Logger.LogInformation("Calling CreateSalesGoalAsync");
        var result = await salesGoalService.CreateSalesGoalAsync(salesGoalDto);

        // Assert
        Logger.LogInformation("Asserting that the sales goal is added to the database");
        result.Should().NotBeNull();
        var dbSalesGoal = await FindAsync<SalesGoal>(result.Id);
        dbSalesGoal.Should().NotBeNull();
        dbSalesGoal!.OwnedById.Should().Be(salesGoalDto.OwnedById);
        dbSalesGoal.GoalDate.Should().Be(salesGoalDto.GoalDate.Value);
        dbSalesGoal.GoalAmount.Should().Be(salesGoalDto.GoalAmount);

        Logger.LogInformation("Sales goal added successfully");
    }

    [Trait("Description", "Verifies that a sales goal is updated in the database")]
    [Fact]
    public async Task UpdateSalesGoalAsync_ShouldUpdateSalesGoalInDatabase()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SalesGoalService and a sales goal DTO");
        var salesGoalService = CreateSalesGoalService();
        var salesGoal = new SalesGoal("Agent1") { GoalDate = new DateTime(2022, 1, 1), GoalAmount = 100000 };
        await AddAsync(salesGoal);
        var salesGoalDto = new SalesGoalDto
        {
            Id = salesGoal.Id,
            OwnedById = "Agent2",
            GoalDate = new DateTime(2022, 2, 1),
            GoalAmount = 200000
        };

        // Act
        Logger.LogInformation("Calling UpdateSalesGoalAsync");
        var result = await salesGoalService.UpdateSalesGoalAsync(salesGoalDto);

        // Assert
        Logger.LogInformation("Asserting that the sales goal is updated in the database");
        result.Should().NotBeNull();
        var dbSalesGoal = await FindAsync<SalesGoal>(result.Id);
        dbSalesGoal.Should().NotBeNull();
        dbSalesGoal!.OwnedById.Should().Be(salesGoalDto.OwnedById);
        dbSalesGoal.GoalDate.Should().Be(salesGoalDto.GoalDate.Value);
        dbSalesGoal.GoalAmount.Should().Be(salesGoalDto.GoalAmount);

        Logger.LogInformation("Sales goal updated successfully");
    }

    [Trait("Description", "Verifies that a sales goal is removed from the database")]
    [Fact]
    public async Task DeleteSalesGoalAsync_ShouldRemoveSalesGoalFromDatabase()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SalesGoalService and a sales goal");
        var salesGoalService = CreateSalesGoalService();
        var salesGoal = new SalesGoal("Agent1") { GoalDate = new DateTime(2022, 1, 1), GoalAmount = 100000 };
        await AddAsync(salesGoal);

        // Act
        Logger.LogInformation("Calling DeleteSalesGoalAsync");
        await salesGoalService.DeleteSalesGoalAsync(salesGoal.Id);

        // Assert
        Logger.LogInformation("Asserting that the sales goal is removed from the database");
        var dbSalesGoal = await FindAsync<SalesGoal>(salesGoal.Id);
        dbSalesGoal.Should().BeNull();

        Logger.LogInformation("Sales goal removed successfully");
    }

    [Trait("Description", "Verifies that the existence of a sales goal is correctly determined")]
    [Theory]
    [InlineData("owner1", "2022/1/1", true)]
    [InlineData("owner1", "2022/2/1", false)]
    [InlineData("nonExistingOwner", "2022/2/1", false)]
    public async Task DoesGoalExistAsync_ShouldCorrectlyDetermineExistenceOfSalesGoal(string ownerId, DateTime goalDate, bool expectedExistence)
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SalesGoalService and adding a sales goal");
        var salesGoalService = CreateSalesGoalService();
        var salesGoal = new SalesGoal("owner1") { GoalDate = new DateTime(2022, 1, 1), GoalAmount = 100000 };
        await AddAsync(salesGoal);

        // Act
        Logger.LogInformation("Calling DoesGoalExistAsync");
        var result = await salesGoalService.DoesGoalExistAsync(ownerId, goalDate);

        // Assert
        Logger.LogInformation("Asserting that the existence of the sales goal is correctly determined");
        result.Should().Be(expectedExistence);
        Logger.LogInformation("Existence of sales goal correctly determined");
    }
}
