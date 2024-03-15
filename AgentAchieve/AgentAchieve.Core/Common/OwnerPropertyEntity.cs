using AgentAchieve.Core.Domain;

namespace AgentAchieve.Core.Common;

/// <summary>
/// Represents an abstract base class for entities that have an owner.
/// </summary>
public abstract class OwnerPropertyEntity(string ownedById) : BaseAuditableEntity
{

    /// <summary>
    /// Gets or sets the application user id who owns the entity.
    /// </summary>
    public virtual string OwnedById { get; set; } = ownedById;

    /// <summary>
    /// Gets or sets the owner of the entity.
    /// </summary>
    public virtual ApplicationUser? OwnedBy { get; set; }
}
