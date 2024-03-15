namespace AgentAchieve.Core.Common;

/// <summary>
/// Represents an entity in the domain.
/// </summary>
public interface IEntity
{
}

/// <summary>
/// Represents an entity in the domain with a specified identifier.
/// </summary>
public interface IEntityPk : IEntity
{
    /// <summary>
    /// Gets or sets the identifier of the entity.
    /// </summary>
    int Id { get; }
}
