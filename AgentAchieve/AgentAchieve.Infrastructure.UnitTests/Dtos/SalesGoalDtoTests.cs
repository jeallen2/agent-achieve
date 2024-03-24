using AgentAchieve.Infrastructure.Features.Sales;
using AgentAchieve.Infrastructure.Features.SalesGoals;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;


namespace AgentAchieve.Infrastructure.UnitTests.Dtos;

public class SalesGoalDtoTests(ITestOutputHelper outputHelper) : LoggingTestBase<SalesGoalDto>(outputHelper)
{
    [Trait("Description", "Verifies that TotalSales calculates correctly")]
    [Fact]
    public void TotalSales_ShouldCalculateCorrectly()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SalesGoalDto and adding sales");
        var salesGoalDto = new SalesGoalDto
        {
            Sales = new List<SaleDto>
            {
                new SaleDto { ClosingDate = new DateTime(2022, 1, 1), SalePrice = 100 },
                new SaleDto { ClosingDate = new DateTime(2022, 1, 31), SalePrice = 200 },
                new SaleDto { ClosingDate = new DateTime(2022, 2, 1), SalePrice = 300 }
            },
            GoalMonthYear = new DateTime(2022, 1, 1)
        };

        // Act
        Logger.LogInformation("Calculating TotalSales");
        var totalSales = salesGoalDto.TotalSales;

        // Assert
        Logger.LogInformation("Asserting that TotalSales calculates correctly");
        Assert.Equal(300, totalSales);
        Logger.LogInformation("TotalSales calculated successfully");
    }


    [Trait("Description", "Verifies that SalesGoalVariance calculates correctly")]
    [Theory]
    [InlineData(500, 300, -200)] // Test with sales goal greater than total sales
    [InlineData(300, 300, 0)] // Test with sales goal equal to total sales
    [InlineData(200, 300, 100)] // Test with sales goal less than total sales
    public void SalesGoalVariance_ShouldCalculateCorrectly(decimal salesGoalAmount, decimal totalSales, decimal expectedVariance)
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SalesGoalDto and adding sales");
        var salesGoalDto = new SalesGoalDto
        {
            SalesGoalAmount = salesGoalAmount,
            Sales = new List<SaleDto>
        {
            new SaleDto { ClosingDate = new DateTime(2022, 1, 1), SalePrice = totalSales / 2 },
            new SaleDto { ClosingDate = new DateTime(2022, 1, 31), SalePrice = totalSales / 2 }
        },
            GoalMonthYear = new DateTime(2022, 1, 1)
        };

        // Act
        Logger.LogInformation("Calculating SalesGoalVariance");
        var salesGoalVariance = salesGoalDto.SalesGoalVariance;

        // Assert
        Logger.LogInformation("Asserting that SalesGoalVariance calculates correctly");
        Assert.Equal(expectedVariance, salesGoalVariance);
        Logger.LogInformation("SalesGoalVariance calculated successfully");
    }

    [Trait("Description", "Verifies that PercentToSalesGoal calculates correctly")]
    [Theory]
    [InlineData(500, 300, 60)] // Test with sales goal greater than total sales
    [InlineData(300, 300, 100)] // Test with sales goal equal to total sales
    [InlineData(200, 300, 150)] // Test with sales goal less than total sales
    public void PercentToSalesGoal_ShouldCalculateCorrectly(decimal salesGoalAmount, decimal totalSales, decimal expectedPercent)
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SalesGoalDto and adding sales");
        var salesGoalDto = new SalesGoalDto
        {
            SalesGoalAmount = salesGoalAmount,
            Sales = new List<SaleDto>
        {
            new SaleDto { ClosingDate = new DateTime(2022, 1, 1), SalePrice = totalSales / 2 },
            new SaleDto { ClosingDate = new DateTime(2022, 1, 31), SalePrice = totalSales / 2 }
        },
            GoalMonthYear = new DateTime(2022, 1, 1)
        };

        // Act
        Logger.LogInformation("Calculating PercentToSalesGoal");
        var percentToSalesGoal = salesGoalDto.PercentToSalesGoal;

        // Assert
        Logger.LogInformation("Asserting that PercentToSalesGoal calculates correctly");
        Assert.Equal(expectedPercent, percentToSalesGoal);
        Logger.LogInformation("PercentToSalesGoal calculated successfully");
    }

}
