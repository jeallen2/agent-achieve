using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Features.Properties;
using AgentAchieve.Infrastructure.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgentAchieve.Infrastructure.Features.Propertys
{
    /// <summary>
    /// Represents a service for managing propertys.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="PropertyService"/> class.
    /// </remarks>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="logger">The logger.</param>
    public class PropertyService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PropertyService> logger) : ServiceBase<Property>(unitOfWork, mapper, logger), IPropertyService
    {

        /// <summary>
        /// Gets all propertys as DTOs asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of property DTOs.</returns>
        public async Task<IEnumerable<PropertyDto>> GetAllDtoAsync()
        {
            return await GetAllDto<PropertyDto>().ToListAsync();
        }

        /// <summary>
        /// Gets a property DTO by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the property.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the property DTO, or null if not found.</returns>
        public async Task<PropertyDto?> GetDtoByIdAsync(int id)
        {
            return await GetDtoByIdAsync<PropertyDto>(id);
        }

        /// <summary>
        /// Creates a new property asynchronously.
        /// </summary>
        /// <param name="propertyDto">The property DTO.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created property DTO.</returns>
        public async Task<PropertyDto> CreatePropertyAsync(PropertyDto propertyDto)
        {
            return await InsertDtoAsync(propertyDto);
        }

        /// <summary>
        /// Updates an existing property asynchronously.
        /// </summary>
        /// <param name="propertyDto">The property DTO.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated property DTO.</returns>
        public async Task<PropertyDto> UpdatePropertyAsync(PropertyDto propertyDto)
        {
            return await UpdateDtoAsync(propertyDto.Id, propertyDto);
        }

        /// <summary>
        /// Deletes a property by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the property to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task DeletePropertyAsync(int id)
        {
            await DeleteAsync(id);
        }
    }
}
