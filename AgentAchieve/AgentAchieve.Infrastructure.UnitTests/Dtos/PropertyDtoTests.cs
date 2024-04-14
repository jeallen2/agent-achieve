using AgentAchieve.Infrastructure.Features.Properties;
using AgentAchieve.UnitTests.Common;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace AgentAchieve.Infrastructure.UnitTests.Dtos;

public class PropertyDtoTests(ITestOutputHelper outputHelper) : LoggingTestBase<PropertyDto>(outputHelper)
{
    [Fact]
    [Trait("Description", "Verifies that FullAddress concatenates correctly")]
    public void FullAddress_ShouldConcatenateCorrectly()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating PropertyDto");
        var propertyDto = new PropertyDto
        {
            Street = "123 Main St",
            City = "Anytown",
            State = "NY",
            ZipCode = "12345"
        };

        // Act
        Logger.LogInformation("Getting FullAddress");
        var fullAddress = propertyDto.FullAddress;

        // Assert
        Logger.LogInformation("Asserting that FullAddress concatenates correctly");
        Assert.Equal("123 Main St Anytown, NY 12345", fullAddress);
        Logger.LogInformation("FullAddress concatenated successfully");
    }
}
