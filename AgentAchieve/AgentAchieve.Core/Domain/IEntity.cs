namespace AgentAchieve.Core.Domain
{
    /// <summary>
    /// Represents an entity in the domain.
    /// </summary>
    public interface IEntity
    {
    }

    /// <summary>
    /// Represents an entity in the domain with a specified identifier.
    /// </summary>
    /// <typeparam name="T">The type of the identifier.</typeparam>
    public interface IEntity<T> : IEntity
    {
        /// <summary>
        /// Gets or sets the identifier of the entity.
        /// </summary>
        T Id { get; set; }
    }
}
