using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Features.Identity;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MockQueryable.Moq;
using Moq;
using Moq.AutoMock;
using System.Security.Claims;
using Xunit.Abstractions;


namespace AgentAchieve.Infrastructure.UnitTests.Services;

/// <summary>
/// Represents the unit tests for the <see cref="IdentityService"/> class.
/// </summary>
public class IdentityServiceTests(ITestOutputHelper outputHelper, DatabaseFixture dbFixture) : TestBase<IdentityService>(outputHelper, dbFixture)
{
    [Trait("Description", "Verifies successful login with an existing external account")]
    [Fact]
    public async Task ProcessExternalLoginAsync_ExistingUser_ReturnsSuccess()
    {
        LogDescription();

        // Arrange
        var email = "test@test.com";
        Logger.LogInformation("Test with email: {Email}", email);
        var info = CreateExternalLoginInfo(email);
        var identityService = CreateIdentityServiceForTest(SignInResult.Success, IdentityResult.Success);

        // Act
        Logger.LogInformation("Calling ProcessExternalLoginAsync");
        var result = await identityService.ProcessExternalLoginAsync(info);
        Logger.LogInformation("ProcessExternalLoginAsync returned status: {Status}", result.Status);

        // Assert
        var expectedStatus = AuthenticationResultStatus.Success;
        result.Status.Should().Be(expectedStatus, "because an existing user should be signed in");
        Logger.LogInformation("Expected status: {ExpectedStatus}, Actual status: {ActualStatus}", expectedStatus, result.Status);

        // Contextual Logging
        Logger.LogInformation("Login successful with status: {Status}", result.Status);
    }


    [Trait("Description", "Verifies a new account is created for a new user login")]
    [Fact]
    public async Task ProcessExternalLoginAsync_NewUser_CreatesAndSignsIn()
    {
        LogDescription();

        // Arrange
        var email = "newuser@test.com";
        Logger.LogInformation("Test with email: {Email}", email);
        var info = CreateExternalLoginInfo(email);
        var identityService = CreateIdentityServiceForTest(SignInResult.Failed, IdentityResult.Success);

        // Act
        Logger.LogInformation("Calling ProcessExternalLoginAsync");
        var result = await identityService.ProcessExternalLoginAsync(info);

        // Assert
        result.Status.Should().Be(AuthenticationResultStatus.NewAccountCreated, "because a new account should be created and signed in");
        Logger.LogInformation("New Account Created - Status: {Status}", result.Status);
    }

    [Trait("Description", "Verifies login failure when no email is provided")]
    [Fact]
    public async Task ProcessExternalLoginAsync_NoEmail_ReturnsFailure()
    {
        LogDescription();

        // Arrange
        var info = new ExternalLoginInfo(new ClaimsPrincipal(), "TestProvider", "12345", "Test User"); // No email
        var identityService = CreateIdentityServiceForTest(SignInResult.Failed, IdentityResult.Failed());

        // Act
        Logger.LogInformation("Calling ProcessExternalLoginAsync");
        var result = await identityService.ProcessExternalLoginAsync(info);

        // Assert
        result.Status.Should().Be(AuthenticationResultStatus.Failure, "because login requires an email");
        Logger.LogInformation("Login Failed - Status: {Status}", result.Status);
    }

    [Trait("Description", "Verifies login failure when account creation fails")]
    [Fact]
    public async Task ProcessExternalLoginAsync_UserCreationFails_ReturnsFailure()
    {
        LogDescription();

        // Arrange
        var email = "newuser2@test.com";
        Logger.LogInformation("Test with email: {Email}", email);
        var info = CreateExternalLoginInfo(email);
        var identityService = CreateIdentityServiceForTest(
            SignInResult.Failed,
            IdentityResult.Failed(new IdentityError { Description = "Test Error" }));

        // Act
        Logger.LogInformation("Calling ProcessExternalLoginAsync");
        var result = await identityService.ProcessExternalLoginAsync(info);

        // Assert
        result.Status.Should().Be(AuthenticationResultStatus.Failure, "because login should fail if account creation fails");
        Logger.LogInformation("Account Creation Failed - Status: {Status}", result.Status);
    }

    [Trait("Description", "Verifies retrieval of all users")]
    [Fact]
    public async Task GetUsers_ShouldReturnListOfApplicationUserDto()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Setting up test data and mocks");
        var autoMocker = new AutoMocker();
        var userManagerMock = autoMocker.GetMock<UserManager<ApplicationUser>>();
        var currentUserServiceMock = autoMocker.GetMock<ICurrentUserService>();
        var signInManagerMock = autoMocker.GetMock<SignInManager<ApplicationUser>>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ApplicationUser, ApplicationUserDto>();
        });

        var mapper = config.CreateMapper();

        var identityService = new IdentityService(signInManagerMock.Object, userManagerMock.Object, Logger, currentUserServiceMock.Object, mapper);

        var users = new List<ApplicationUser>
    {
        new ApplicationUser { Id = "1", UserName = "user1" },
        new ApplicationUser { Id = "2", UserName = "user2" },
        new ApplicationUser { Id = "3", UserName = "user3" }
    };

        var mockUsers = users.AsQueryable().BuildMockDbSet();
        userManagerMock.Setup(u => u.Users).Returns(mockUsers.Object);

        // Act
        Logger.LogInformation("Calling GetUsers");
        var result = await identityService.GetUsers();

        // Assert
        Logger.LogInformation("Asserting results");
        var expectedUsers = mapper.Map<List<ApplicationUserDto>>(users);
        result.Should().HaveCount(expectedUsers.Count, "because all users should be returned");
        result.Should().OnlyContain(u => expectedUsers.Any(eu => eu.Id == u.Id && eu.UserName == u.UserName), "because the returned users should match the test data");

        // Contextual Logging
        Logger.LogInformation("GetUsers returned {Count} users", result.Count());
    }

    /// <summary>
    /// Represents information about an external login.
    /// </summary>
    /// <param name="email">The email associated with the external login.</param>
    /// <returns>An instance of ExternalLoginInfo.</returns>
    private static ExternalLoginInfo CreateExternalLoginInfo(string email)
    {
        return new ExternalLoginInfo(new ClaimsPrincipal(), "TestProvider", "12345", "Test User")
        {
            Principal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }))
        };
    }


    /// <summary>
    /// Creates an instance of IdentityService for testing, with mocked dependencies.
    /// </summary>
    /// <param name="signInResult">The result to be returned when SignInManager's ExternalLoginSignInAsync method is called.</param>
    /// <param name="createUserResult">The result to be returned when UserManager's CreateAsync method is called.</param>
    /// <returns>An instance of IdentityService with mocked dependencies.</returns>
    /// <remarks>
    /// This method sets up mocks for SignInManager and UserManager, which are dependencies of IdentityService.
    /// The SignInManager mock is set up to return the provided signInResult when its ExternalLoginSignInAsync method is called.
    /// The UserManager mock is set up to return the provided createUserResult when its CreateAsync method is called.
    /// These mocks allow us to control the behavior of IdentityService's dependencies during testing, making our tests more reliable and easier to understand.
    /// </remarks>
    private IdentityService CreateIdentityServiceForTest(SignInResult signInResult, IdentityResult createUserResult)
    {
        Logger.LogInformation("Setting up SignInManager mock with result: {Result}", signInResult);
        AutoMocker.GetMock<SignInManager<ApplicationUser>>()
            .Setup(s => s.ExternalLoginSignInAsync(It.IsAny<string>(), It.IsAny<string>(), false, true))
            .ReturnsAsync(signInResult);

        Logger.LogInformation("Setting up UserManager mock with result: {Result}", createUserResult);
        AutoMocker.GetMock<UserManager<ApplicationUser>>()
            .Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>()))
            .ReturnsAsync(createUserResult);

        return AutoMocker.CreateInstance<IdentityService>();
    }
}