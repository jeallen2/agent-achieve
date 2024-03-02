using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Services;

namespace AgentAchieve.Infrastructure.Features.Clients;

/// <summary>
/// Represents a service for managing clients.
/// </summary>
public interface IClientService : IServiceBase<Client>
{
    /// <summary>
    /// Retrieves all clients as data transfer objects.
    /// </summary>
    /// <returns>An asynchronous operation that returns a collection of client data transfer objects.</returns>
    Task<IEnumerable<ClientDto>> GetAllDtoAsync();

    /// <summary>
    /// Retrieves a client data transfer object by its ID.
    /// </summary>
    /// <param name="id">The ID of the client.</param>
    /// <returns>An asynchronous operation that returns the client data transfer object, or null if not found.</returns>
    Task<ClientDto?> GetDtoByIdAsync(int id);

    /// <summary>
    /// Creates a new client using the provided client data transfer object.
    /// </summary>
    /// <param name="clientDto">The client data transfer object.</param>
    /// <returns>An asynchronous operation that returns the created client data transfer object.</returns>
    Task<ClientDto> CreateClientAsync(ClientDto clientDto);

    /// <summary>
    /// Updates an existing client using the provided client data transfer object.
    /// </summary>
    /// <param name="clientDto">The client data transfer object.</param>
    /// <returns>An asynchronous operation that returns the updated client data transfer object.</returns>
    Task<ClientDto> UpdateClientAsync(ClientDto clientDto);

    /// <summary>
    /// Deletes a client by its ID.
    /// </summary>
    /// <param name="id">The ID of the client to delete.</param>
    /// <returns>An asynchronous operation.</returns>
    Task DeleteClientAsync(int id);
}
