using AgentAchieve.WebUi.Server.Components.Pages.SalesGoals;
using AgentAchieve.WebUi.Server.UnitTests.Common;
using Bunit;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace AgentAchieve.WebUi.Server.UnitTests;

public class SalesGoalsTests(ITestOutputHelper outputHelper) : TelerikTestContext(outputHelper)
{
    [Fact]
    [Trait("Description", "Verifies that the grid has an Excel export button")]
    public void GridHasExcelExportButton()
    {
        LogDescription();

        // Arrange

        // Act
        Logger.LogInformation("Rendering SalesGoals component");
        var cut = RenderComponent<SalesGoals>();

        // Assert
        Logger.LogInformation("Finding grid toolbar and Excel export button");
        var gridToolbar = cut.Find(".k-grid-toolbar");
        Assert.NotNull(gridToolbar.QuerySelector("button:contains('Export to Excel')"));

        Logger.LogInformation("The grid has an Excel export button");
    }

    [Fact]
    [Trait("Description", "Verifies that the grid is displayed when the 'Grid' option is selected")]
    public void GridIsDisplayedWhenGridOptionSelected()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Rendering SalesGoals component");
        var cut = RenderComponent<SalesGoals>();

        // Act
        Logger.LogInformation("Simulating 'Grid' option selection");
        var gridButton = cut.FindAll("ButtonGroupToggleButton").FirstOrDefault(button => button.TextContent.Trim() == "Grid");
        gridButton?.Click();

        // Assert
        Logger.LogInformation("Checking if the grid is displayed and the chart is not displayed");
        Assert.NotNull(cut.Find("#gridWrapper")); // Assert that the grid is displayed
        Assert.Throws<ElementNotFoundException>(() => cut.Find("#chartWrapper")); // Assert that the chart is not displayed
        Logger.LogInformation("The grid is displayed and the chart is not displayed");
    }

    [Fact]
    [Trait("Description", "Verifies that the chart is displayed when the 'Chart' option is selected")]
    public void ChartIsDisplayedWhenChartOptionSelected()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Rendering SalesGoals component");
        var cut = RenderComponent<SalesGoals>();

        // Act
        Logger.LogInformation("Simulating 'Chart' option selection");
        var buttons = cut.FindAll(".k-button-group button");
        var chartButton = buttons.FirstOrDefault(button => button.TextContent.Trim() == "Chart");
        chartButton?.Click();

        // Assert
        Logger.LogInformation("Checking if the chart is displayed and the grid is not displayed");
        Assert.NotNull(cut.Find("#chartWrapper")); // Assert that the chart is displayed
        Assert.Throws<ElementNotFoundException>(() => cut.Find("#gridWrapper")); // Assert that the grid is not displayed
        Logger.LogInformation("The chart is displayed and the grid is not displayed");
    }

}