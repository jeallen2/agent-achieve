namespace AgentAchieve.Infrastructure.Features.Identity;
/// <summary>
/// Represents a service for retrieving information about the current user.
/// </summary>
public interface ICurrentUserService
{

    /// <summary>
    /// Asynchronously gets the ID of the current user.
    /// </summary>
    /// <returns>The ID of the current user, or null if the user is not authenticated.</returns>
    Task<string?> GetUserIdAsync();

    /// <summary>
    /// Asynchronously gets the name of the current user.
    /// </summary>
    /// <returns>The name of the current user, or null if the user is not authenticated.</returns>
    Task<string?> GetUserNameAsync();
}
