using Microsoft.AspNetCore.Identity;

namespace AgentAchieve.Infrastructure.Identity
{
    /// <summary>
    /// Actions related to managing authentication (and later additional user operations)
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Handles external login processing for providers configured through Identity (Google, etc.)
        /// </summary>
        /// <param name="info">The login information obtained from the external provider</param>
        /// <returns>An AuthenticationResult describing the operation's status</returns>
        Task<AuthenticationResult> ProcessExternalLoginAsync(ExternalLoginInfo info);
    }
}