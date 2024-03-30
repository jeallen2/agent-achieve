using AgentAchieve.Core.Domain;
using AgentAchieve.UnitTests.Common;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;


namespace AgentAchieve.Core.UnitTests;
public class ApplicationUserTests(ITestOutputHelper outputHelper) : LoggingTestBase<ApplicationUser>(outputHelper)
{
    [Trait("Description", "Verifies that FullName concatenates correctly")]
    [Theory]
    [InlineData("John", "Doe", "jdoe", "Doe, John")]
    [InlineData(null, "Doe", "jdoe", "Doe")]
    [InlineData("John", null, "jdoe", "John")]
    [InlineData(null, null, "jdoe", "jdoe")]
    public void FullName_ReturnsCorrectFormat(string? firstName, string? lastName, string userName, string expectedFullName)
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating ApplicationUser");
        var user = new ApplicationUser
        {
            FirstName = firstName,
            LastName = lastName,
            UserName = userName
        };

        // Act
        Logger.LogInformation("Getting FullName");
        var fullName = user.FullName;

        // Assert
        Logger.LogInformation("Asserting that FullName concatenates correctly");
        Assert.Equal(expectedFullName, fullName);
        Logger.LogInformation("FullName concatenated successfully");
    }
}
