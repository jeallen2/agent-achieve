using AgentAchieve.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace AgentAchieve.Infrastructure.Features.Identity;

/// <summary>
/// Contains statuses for authentication operation results
/// </summary>
public enum AuthenticationResultStatus
{
    /// <summary>
    /// Auth operation was successful
    /// </summary>
    Success,
    /// <summary>
    /// A new account was created
    /// </summary>
    NewAccountCreated,
    /// <summary>
    /// Auth operation failed
    /// </summary>
    Failure
}

/// <summary>
/// Represents the outcome of an identity operation in more detail
/// </summary>
public class AuthenticationResult
{
    public AuthenticationResultStatus Status { get; set; }
    public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
}

/// <summary>
/// Service providing methods related to Authentication actions 
/// (and in the future, other user or role operations)
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="IdentityService"/> class.
/// </remarks>
/// <param name="signInManager">The sign-in manager.</param>
/// <param name="userManager">The user manager.</param>
/// <param name="logger">The logger.</param>
public class IdentityService(
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    ILogger<IdentityService> logger,
    ICurrentUserService currentUserService) : IIdentityService
{
    internal readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    internal readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ILogger<IdentityService> _logger = logger;
    private readonly ICurrentUserService currentUserService = currentUserService;

    /// <summary>
    /// Processes the external login asynchronously.
    /// </summary>
    /// <param name="info">The external login information.</param>
    /// <returns>The authentication result.</returns>
    public async Task<AuthenticationResult> ProcessExternalLoginAsync(ExternalLoginInfo info)
    {
        var signInResult = await TrySignInWithExistingAccount(info);
        if (signInResult.Succeeded)
        {
            return new AuthenticationResult { Status = AuthenticationResultStatus.Success };
        }

        return await CreateAndSignInNewAccount(info);
    }

    /// <summary>
    /// Attempts to sign in using an existing account linked to the external provider.
    /// </summary>
    /// <param name="info">The external login information.</param>
    /// <returns>The sign-in result.</returns>
    private async Task<SignInResult> TrySignInWithExistingAccount(ExternalLoginInfo info)
    {
        return await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false, true);
    }

    /// <summary>
    /// Creates a new account using external login information and signs the user in.
    /// </summary>
    /// <param name="info">The external login information.</param>
    /// <returns>The authentication result.</returns>
    private async Task<AuthenticationResult> CreateAndSignInNewAccount(ExternalLoginInfo info)
    {
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email))
        {
            var errorMessage = "Error: No email returned from external provider.";
            _logger.LogError(errorMessage);
            return new AuthenticationResult
            {
                Status = AuthenticationResultStatus.Failure,
                Errors = [errorMessage]
            };
        }

        // Create new user
        var user = new ApplicationUser { UserName = email, Email = email };
        var createResult = await _userManager.CreateAsync(user);

        if (createResult.Succeeded)
        {
            await _userManager.AddLoginAsync(user, info);
            await _signInManager.SignInAsync(user, false, info.LoginProvider);
            return new AuthenticationResult { Status = AuthenticationResultStatus.NewAccountCreated };
        }

        return new AuthenticationResult
        {
            Status = AuthenticationResultStatus.Failure,
            Errors = createResult.Errors.Select(e => "Error: " + e.Description)
        };
    }
}
