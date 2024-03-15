using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AgentAchieve.Core.Domain;

/// <summary>
/// Represents an application user.
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the sales associated with the user.
    /// </summary>
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
