namespace AgentAchieve.Core.Common;

/// <summary>
/// Represents a base class for auditable entities.
/// </summary>
/// <typeparam name="T">The type of the entity's identifier.</typeparam>
public abstract class BaseAuditableEntity<T> : IEntity<T>
{
    /// <summary>
    /// Gets or sets the identifier of the entity.
    /// </summary>
    public virtual T Id { get; private set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public virtual DateTime? Created { get; set; }

    /// <summary>
    /// Gets or sets the user who created the entity.
    /// </summary>
    public virtual string? CreatedBy { get; set; }

    /// <summary>   
    /// Gets or sets the date and time when the entity was last modified.
    /// </summary>
    public virtual DateTime? LastModified { get; set; }

    /// <summary>
    /// Gets or sets the user who last modified the entity.
    /// </summary>
    public virtual string? LastModifiedBy { get; set; }
}