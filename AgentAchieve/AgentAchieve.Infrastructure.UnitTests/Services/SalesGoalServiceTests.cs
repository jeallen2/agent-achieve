using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Features.SalesGoals;
using FluentAssertions;
using AgentAchieve.UnitTests.Common;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace AgentAchieve.Infrastructure.UnitTests.Services;
public class SalesGoalServiceTests(ITestOutputHelper outputHelper, DatabaseFixture dbFixture) : TestBase<SalesGoalService>(outputHelper, dbFixture)
{
    [Trait("Description", "Verifies that all sales goals are returned")]
    [Fact]
    public async Task GetAllDtoAsync_ShouldReturnAllSalesGoals()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SalesGoalService and adding sales goals");
        var applicationUser = new ApplicationUser { Id = "Agent1" };
        var applicationUser2 = new ApplicationUser { Id = "Agent2" };
        await AddAsync(applicationUser);
        await AddAsync(applicationUser2);
        var salesGoalService = CreateSalesGoalService();
        var salesGoal1 = new SalesGoal("Agent1") { GoalMonthYear = new DateTime(2022, 1, 1), SalesGoalAmount = 100000 };
        var salesGoal2 = new SalesGoal("Agent2") { GoalMonthYear = new DateTime(2022, 2, 1), SalesGoalAmount = 200000 };

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
        var applicationUser = new ApplicationUser { Id = "Agent1" };
        await AddAsync(applicationUser);

        var salesGoalService = CreateSalesGoalService();
        var salesGoal = new SalesGoal("Agent1") { GoalMonthYear = new DateTime(2022, 1, 1), SalesGoalAmount = 100000 };
        await AddAsync(salesGoal);

        // Act
        Logger.LogInformation("Calling GetDtoByIdAsync");
        var result = await salesGoalService.GetDtoByIdAsync(salesGoal.Id);

        // Assert
        Logger.LogInformation("Asserting that the correct sales goal is returned");
        result.Should().NotBeNull();
        result!.Id.Should().Be(salesGoal.Id);
        result.OwnedById.Should().Be(salesGoal.OwnedById);
        result.GoalMonthYear.Should().Be(salesGoal.GoalMonthYear);
        result.SalesGoalAmount.Should().Be(salesGoal.SalesGoalAmount);
        Logger.LogInformation("Correct sales goal returned successfully");
    }

    [Trait("Description", "Verifies that a sales goal is added to the database")]
    [Fact]
    public async Task CreateSalesGoalAsync_ShouldAddSalesGoalToDatabase()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SalesGoalService and a sales goal DTO");
        var applicationUser = new ApplicationUser { Id = "Agent1" };
        await AddAsync(applicationUser);

        var salesGoalService = CreateSalesGoalService();
        var salesGoalDto = new SalesGoalDto
        {
            OwnedById = "Agent1",
            GoalMonthYear = new DateTime(2022, 1, 1),
            SalesGoalAmount = 100000
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
        dbSalesGoal.GoalMonthYear.Should().Be(salesGoalDto.GoalMonthYear.Value);
        dbSalesGoal.SalesGoalAmount.Should().Be(salesGoalDto.SalesGoalAmount);

        Logger.LogInformation("Sales goal added successfully");
    }

    [Trait("Description", "Verifies that a sales goal is updated in the database")]
    [Fact]
    public async Task UpdateSalesGoalAsync_ShouldUpdateSalesGoalInDatabase()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SalesGoalService and a sales goal DTO");
        var applicationUser = new ApplicationUser { Id = "Agent1" };
        var applicationUser2 = new ApplicationUser { Id = "Agent2" };
        await AddAsync(applicationUser);
        await AddAsync(applicationUser2);

        var salesGoalService = CreateSalesGoalService();
        var salesGoal = new SalesGoal("Agent1") { GoalMonthYear = new DateTime(2022, 1, 1), SalesGoalAmount = 100000 };
        await AddAsync(salesGoal);
        var salesGoalDto = new SalesGoalDto
        {
            Id = salesGoal.Id,
            OwnedById = "Agent2",
            GoalMonthYear = new DateTime(2022, 2, 1),
            SalesGoalAmount = 200000
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
        dbSalesGoal.GoalMonthYear.Should().Be(salesGoalDto.GoalMonthYear.Value);
        dbSalesGoal.SalesGoalAmount.Should().Be(salesGoalDto.SalesGoalAmount);

        Logger.LogInformation("Sales goal updated successfully");
    }

    [Trait("Description", "Verifies that a sales goal is removed from the database")]
    [Fact]
    public async Task DeleteSalesGoalAsync_ShouldRemoveSalesGoalFromDatabase()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SalesGoalService and a sales goal");
        var applicationUser = new ApplicationUser { Id = "Agent1" };
        await AddAsync(applicationUser);

        var salesGoalService = CreateSalesGoalService();
        var salesGoal = new SalesGoal("Agent1") { GoalMonthYear = new DateTime(2022, 1, 1), SalesGoalAmount = 100000 };
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
    [InlineData("owner1", "2022/1/1", 0, true)]
    [InlineData("owner1", "2022/2/1", 0, false)]
    [InlineData("nonExistingOwner", "2022/2/1", 0, false)]
    [InlineData("owner1", "2022/1/1", 1, false)] // Test case where an update doesn't block against itself
    public async Task DoesGoalExistAsync_ShouldCorrectlyDetermineExistenceOfSalesGoal(string ownerId, DateTime goalMonthYear, int id, bool expectedExistence)
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SalesGoalService and adding a sales goal");
        var applicationUser = new ApplicationUser { Id = "owner1" };
        await AddAsync(applicationUser);

        var salesGoalService = CreateSalesGoalService();
        var salesGoal = new SalesGoal("owner1") { GoalMonthYear = new DateTime(2022, 1, 1), SalesGoalAmount = 100000 };
        await AddAsync(salesGoal);

        var salesGoalDto = new SalesGoalDto
        {
            Id = id,
            OwnedById = ownerId,
            GoalMonthYear = goalMonthYear,
            SalesGoalAmount = 100000
        };

        // Act
        Logger.LogInformation("Calling DoesGoalExistAsync");
        var result = await salesGoalService.DoesGoalExistAsync(salesGoalDto);

        // Assert
        Logger.LogInformation("Asserting that the existence of the sales goal is correctly determined");
        result.Should().Be(expectedExistence);
        Logger.LogInformation("Existence of sales goal correctly determined");
    }

}
