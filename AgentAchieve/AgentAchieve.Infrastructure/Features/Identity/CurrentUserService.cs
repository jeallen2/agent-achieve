using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AgentAchieve.Infrastructure.Features.Identity;

/// <summary>
/// Service for retrieving information about the current user.
/// </summary>
public class CurrentUserService : ICurrentUserService
{

    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Gets the ID of the current user asynchronously.
    /// </summary>
    /// <returns>The ID of the current user, or null if the user is not authenticated.</returns>
    public Task<string?> GetUserIdAsync()
    {
        return Task.FromResult(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier));
    }

    /// <summary>
    /// Gets the name of the current user asynchronously.
    /// </summary>
    /// <returns>The name of the current user, or null if the user is not authenticated.</returns>
    public Task<string?> GetUserNameAsync()
    {
        return Task.FromResult(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name) ?? _httpContextAccessor.HttpContext?.User?.Identity?.Name);
    }
}
