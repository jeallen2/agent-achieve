using AgentAchieve.Infrastructure.Features.Clients;
using AgentAchieve.Infrastructure.Features.Sales;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;


namespace AgentAchieve.Infrastructure.UnitTests.Dtos;

public class ClientDtoTests(ITestOutputHelper outputHelper) : LoggingTestBase<SaleDto>(outputHelper)
{

    [Trait("Description", "Verifies that FullNameAndPhone concatenates correctly")]
    [Fact]
    public void FullNameAndPhone_ShouldConcatenateCorrectly()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating ClientDto");
        var clientDto = new ClientDto
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890"
        };

        // Act
        Logger.LogInformation("Getting FullNameAndPhone");
        var fullNameAndPhone = clientDto.FullNameAndPhone;

        // Assert
        Logger.LogInformation("Asserting that FullNameAndPhone concatenates correctly");
        Assert.Equal("Doe, John; Phone: 1234567890", fullNameAndPhone);
        Logger.LogInformation("FullNameAndPhone concatenated successfully");
    }

}
