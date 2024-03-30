using AgentAchieve.Infrastructure.Features.Identity;
using Microsoft.AspNetCore.Http;
using AgentAchieve.UnitTests.Common;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;
using Xunit.Abstractions;

namespace AgentAchieve.Infrastructure.UnitTests.Services;

/// <summary>
/// Represents the unit tests for the <see cref="CurrentUserService"/> class.
/// </summary>
public class CurrentUserServiceTests(ITestOutputHelper outputHelper, DatabaseFixture dbFixture) : TestBase<CurrentUserService>(outputHelper, dbFixture)
{
    [Trait("Description", "Verifies that the user ID is returned when the user is authenticated")]
    [Fact]
    public async Task GetUserIdAsync_WhenUserIsAuthenticated_ReturnsUserId()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating CurrentUserService and setting up user context");
        var userId = "123";
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
                new Claim(ClaimTypes.NameIdentifier, userId)
        }));
        httpContextAccessor.Setup(x => x.HttpContext!.User).Returns(user);

        var currentUserService = new CurrentUserService(httpContextAccessor.Object);

        // Act
        Logger.LogInformation("Calling GetUserIdAsync");
        var result = await currentUserService.GetUserIdAsync();

        // Assert
        Logger.LogInformation("Asserting that the user ID is returned");
        Assert.Equal(userId, result);
        Logger.LogInformation("User ID returned successfully");
    }

    [Trait("Description", "Verifies that null is returned when the user is not authenticated")]
    [Fact]
    public async Task GetUserIdAsync_WhenUserIsNotAuthenticated_ReturnsNull()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating CurrentUserService and setting up user context");
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor.Setup(x => x.HttpContext!.User).Returns((ClaimsPrincipal)null!);

        var currentUserService = new CurrentUserService(httpContextAccessor.Object);

        // Act
        Logger.LogInformation("Calling GetUserIdAsync");
        var result = await currentUserService.GetUserIdAsync();

        // Assert
        Logger.LogInformation("Asserting that null is returned");
        Assert.Null(result);
        Logger.LogInformation("Null returned successfully");
    }

    [Trait("Description", "Verifies that the user name is returned when the user is authenticated")]
    [Fact]
    public async Task GetUserNameAsync_WhenUserIsAuthenticated_ReturnsUserName()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating CurrentUserService and setting up user context");
        var userName = "John Doe";
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
                new Claim(ClaimTypes.Name, userName)
        }));
        httpContextAccessor.Setup(x => x.HttpContext!.User).Returns(user);

        var currentUserService = new CurrentUserService(httpContextAccessor.Object);

        // Act
        Logger.LogInformation("Calling GetUserNameAsync");
        var result = await currentUserService.GetUserNameAsync();

        // Assert
        Logger.LogInformation("Asserting that the user name is returned");
        Assert.Equal(userName, result);
        Logger.LogInformation("User name returned successfully");
    }

    [Trait("Description", "Verifies that null is returned when the user is not authenticated")]
    [Fact]
    public async Task GetUserNameAsync_WhenUserIsNotAuthenticated_ReturnsNull()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating CurrentUserService and setting up user context");
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor.Setup(x => x.HttpContext!.User).Returns((ClaimsPrincipal)null!);

        var currentUserService = new CurrentUserService(httpContextAccessor.Object);

        // Act
        Logger.LogInformation("Calling GetUserNameAsync");
        var result = await currentUserService.GetUserNameAsync();

        // Assert
        Logger.LogInformation("Asserting that null is returned");
        Assert.Null(result);
        Logger.LogInformation("Null returned successfully");
    }
}
