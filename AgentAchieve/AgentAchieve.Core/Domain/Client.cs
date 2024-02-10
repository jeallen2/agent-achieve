namespace AgentAchieve.Core.Domain
{
    /// <summary>
    /// Represents a client entity.
    /// </summary>
    public class Client : IEntity<int>
    {
        /// <summary>
        /// Gets or sets the client ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        public string? Name { get; set; }
    }
}
