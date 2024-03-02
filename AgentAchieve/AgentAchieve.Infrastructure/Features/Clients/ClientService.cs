using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgentAchieve.Infrastructure.Features.Clients
{
    /// <summary>
    /// Represents a service for managing clients.
    /// </summary>
    public class ClientService : ServiceBase<Client>, IClientService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        public ClientService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ClientService> logger)
            : base(unitOfWork, mapper, logger) { }

        /// <summary>
        /// Gets all clients as DTOs asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of client DTOs.</returns>
        public async Task<IEnumerable<ClientDto>> GetAllDtoAsync()
        {
            return await GetAllDto<ClientDto>().ToListAsync();
        }

        /// <summary>
        /// Gets a client DTO by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the client.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the client DTO, or null if not found.</returns>
        public async Task<ClientDto?> GetDtoByIdAsync(int id)
        {
            return await GetDtoByIdAsync<ClientDto>(id);
        }

        /// <summary>
        /// Creates a new client asynchronously.
        /// </summary>
        /// <param name="clientDto">The client DTO.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created client DTO.</returns>
        public async Task<ClientDto> CreateClientAsync(ClientDto clientDto)
        {
            return await InsertDtoAsync(clientDto);
        }

        /// <summary>
        /// Updates an existing client asynchronously.
        /// </summary>
        /// <param name="clientDto">The client DTO.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated client DTO.</returns>
        public async Task<ClientDto> UpdateClientAsync(ClientDto clientDto)
        {
            return await UpdateDtoAsync(clientDto.Id, clientDto);
        }

        /// <summary>
        /// Deletes a client by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the client to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task DeleteClientAsync(int id)
        {
            await DeleteAsync(id);
        }
    }
}
