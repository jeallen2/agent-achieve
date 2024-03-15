using AgentAchieve.Core.Domain;

namespace AgentAchieve.Core.Common;

/// <summary>
/// Represents a base class for auditable entities.
/// </summary>
/// <typeparam name="T">The type of the entity's identifier.</typeparam>
public abstract class BaseAuditableEntity: IEntityPk
{
    /// <summary>
    /// Gets or sets the identifier of the entity.
    /// </summary>
    public virtual int Id { get; protected set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public virtual DateTime? Created { get; set; }

 
    /// <summary>
    /// Gets or sets the application user id who created the entity.
    /// </summary>
    public virtual string? CreatedById { get; set; }

    /// <summary>
    /// Gets or sets the application user who created the entity.
    /// </summary>
    public virtual ApplicationUser? CreatedBy { get; set; }

    /// <summary>   
    /// Gets or sets the date and time when the entity was last modified.
    /// </summary>
    public virtual DateTime? LastModified { get; set; }

    /// <summary>
    /// Gets or sets the application user id who last modified the entity.
    /// </summary>
    public virtual string? LastModifiedById { get; set; }

    /// <summary>
    /// Gets or sets the application user who last modified the entity.
    /// </summary>
    public virtual ApplicationUser? LastModifiedBy { get; set; }
}