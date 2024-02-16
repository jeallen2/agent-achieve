using AgentAchieve.Infrastructure.Identity;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using System.Security.Claims;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace AgentAchieve.Infrastructure.UnitTests
{
    public class IdentityServiceTests : TestBase<IdentityService>
    {
        public IdentityServiceTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Trait("Description", "Verifies successful login with an existing external account")]
        [Fact]
        public async Task ProcessExternalLoginAsync_ExistingUser_ReturnsSuccess()
        {
            LogDescription();

            // Setup
            var email = "test@test.com";
            Logger.LogInformation("Test with email: {Email}", email);
            var info = CreateExternalLoginInfo(email);
            var identityService = CreateIdentityServiceForTest(SignInResult.Success, IdentityResult.Success);

            // Execution
            Logger.LogInformation("Calling ProcessExternalLoginAsync");
            var result = await identityService.ProcessExternalLoginAsync(info);
            Logger.LogInformation("ProcessExternalLoginAsync returned status: {Status}", result.Status);

            // Assertions
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

            // Setup
            var email = "newuser@test.com";
            Logger.LogInformation("Test with email: {Email}", email);
            var info = CreateExternalLoginInfo(email);
            var identityService = CreateIdentityServiceForTest(SignInResult.Failed, IdentityResult.Success);

            // Execution
            Logger.LogInformation("Calling ProcessExternalLoginAsync");
            var result = await identityService.ProcessExternalLoginAsync(info);

            // Assertions
            result.Status.Should().Be(AuthenticationResultStatus.NewAccountCreated, "because a new account should be created and signed in");

            // Contextual Logging
            Logger.LogInformation("New Account Created - Status: {Status}", result.Status);
        }

        [Trait("Description", "Verifies login failure when no email is provided")]
        [Fact]
        public async Task ProcessExternalLoginAsync_NoEmail_ReturnsFailure()
        {
            LogDescription();

            // Setup
            var info = new ExternalLoginInfo(new ClaimsPrincipal(), "TestProvider", "12345", "Test User"); // No email
            var identityService = CreateIdentityServiceForTest(SignInResult.Failed, IdentityResult.Failed());

            // Execution
            Logger.LogInformation("Calling ProcessExternalLoginAsync");
            var result = await identityService.ProcessExternalLoginAsync(info);

            // Assertions
            result.Status.Should().Be(AuthenticationResultStatus.Failure, "because login requires an email");

            // Contextual Logging
            Logger.LogInformation("Login Failed - Status: {Status}", result.Status);
        }

        [Trait("Description", "Verifies login failure when account creation fails")]
        [Fact]
        public async Task ProcessExternalLoginAsync_UserCreationFails_ReturnsFailure()
        {
            LogDescription();

            // Setup
            var email = "newuser2@test.com";
            Logger.LogInformation("Test with email: {Email}", email);
            var info = CreateExternalLoginInfo(email);
            var identityService = CreateIdentityServiceForTest(
                SignInResult.Failed,
                IdentityResult.Failed(new IdentityError { Description = "Test Error" }));

            // Execution
            Logger.LogInformation("Calling ProcessExternalLoginAsync");
            var result = await identityService.ProcessExternalLoginAsync(info);

            // Assertions
            result.Status.Should().Be(AuthenticationResultStatus.Failure, "because login should fail if account creation fails");

            // Contextual Logging
            Logger.LogInformation("Account Creation Failed - Status: {Status}", result.Status);
        }


        private ExternalLoginInfo CreateExternalLoginInfo(string email)
        {
            return new ExternalLoginInfo(new ClaimsPrincipal(), "TestProvider", "12345", "Test User")
            {
                Principal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }))
            };
        }

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
}