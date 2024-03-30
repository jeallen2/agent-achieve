using AgentAchieve.Infrastructure.Features.Sales;
using AgentAchieve.Infrastructure.Features.SalesGoals;
using AgentAchieve.UnitTests.Common;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;


namespace AgentAchieve.Infrastructure.UnitTests.Dtos;

public class SalesGoalDtoTests(ITestOutputHelper outputHelper) : LoggingTestBase<SalesGoalDto>(outputHelper)
{
    [Theory]
    [InlineData("2022-01-01", 300)] // Test with GoalMonthYear set to January 2022
    [InlineData("2022-02-01", 400)] // Test with GoalMonthYear set to February 2022
    [InlineData(null, 0)] // Test with GoalMonthYear set to null
    public void TotalSales_ShouldCalculateCorrectly(string? goalMonthYearStr, decimal expectedTotalSales)
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SalesGoalDto and adding sales");
        DateTime? goalMonthYear = string.IsNullOrEmpty(goalMonthYearStr) ? null : DateTime.Parse(goalMonthYearStr);

        var salesGoalDto = new SalesGoalDto
        {
            Sales = new List<SaleDto>
            {
                new SaleDto { ClosingDate = new DateTime(2022, 1, 1), SalePrice = 100 },
                new SaleDto { ClosingDate = new DateTime(2022, 1, 31), SalePrice = 200 },
                new SaleDto { ClosingDate = new DateTime(2022, 2, 1), SalePrice = 400 }
            },
            GoalMonthYear = goalMonthYear
        };

        // Act
        Logger.LogInformation("Calculating TotalSales");
        var totalSales = salesGoalDto.TotalSales;

        // Assert
        Logger.LogInformation("Asserting that TotalSales calculates correctly");
        Assert.Equal(expectedTotalSales, totalSales);
        Logger.LogInformation("TotalSales calculated successfully");
    }


    [Trait("Description", "Verifies that SalesGoalVariance calculates correctly")]
    [Theory]
    [InlineData(500, 300, -200)] // Test with sales goal greater than total sales
    [InlineData(300, 300, 0)] // Test with sales goal equal to total sales
    [InlineData(200, 300, 100)] // Test with sales goal less than total sales
    [InlineData(null, 300, 0)] // Test with null SalesGoalAmount
    public void SalesGoalVariance_ShouldCalculateCorrectly(object? salesGoalAmountObj, decimal totalSales, decimal expectedVariance)
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SalesGoalDto and adding sales");
        decimal? salesGoalAmount = salesGoalAmountObj == null ? null : Convert.ToDecimal(salesGoalAmountObj);

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
    [InlineData(null, 300, 0)] // Test with null SalesGoalAmount
    public void PercentToSalesGoal_ShouldCalculateCorrectly(object? salesGoalAmountObj, decimal totalSales, decimal expectedPercent)
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SalesGoalDto and adding sales");
        decimal? salesGoalAmount = salesGoalAmountObj == null ? null : Convert.ToDecimal(salesGoalAmountObj);
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
