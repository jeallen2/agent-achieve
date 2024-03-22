using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgentAchieve.Infrastructure.Features.Sales
{
    /// <summary>
    /// Represents a service for managing sales.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="SaleService"/> class.
    /// </remarks>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="logger">The logger.</param>
    public class SaleService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SaleService> logger) : ServiceBase<Sale>(unitOfWork, mapper, logger), ISaleService
    {

        /// <summary>
        /// Gets all sales as DTOs asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of sale DTOs.</returns>
        public async Task<IEnumerable<SaleDto>> GetAllDtoAsync()
        {
            return await GetAllDto<SaleDto>().ToListAsync();
        }

        /// <summary>
        /// Gets a sale DTO by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the sale.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the sale DTO, or null if not found.</returns>
        public async Task<SaleDto?> GetDtoByIdAsync(int id)
        {
            return await GetDtoByIdAsync<SaleDto>(id);
        }

        /// <summary>
        /// Creates a new sale asynchronously.
        /// </summary>
        /// <param name="saleDto">The sale DTO.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created sale DTO.</returns>
        public async Task<SaleDto> CreateSaleAsync(SaleDto saleDto)
        {
            return await InsertDtoAsync(saleDto);
        }

        /// <summary>
        /// Updates an existing sale asynchronously.
        /// </summary>
        /// <param name="saleDto">The sale DTO.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated sale DTO.</returns>
        public async Task<SaleDto> UpdateSaleAsync(SaleDto saleDto)
        {
            return await UpdateDtoAsync(saleDto.Id, saleDto);
        }

        /// <summary>
        /// Deletes a sale by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the sale to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task DeleteSaleAsync(int id)
        {
            await DeleteAsync(id);
        }
    }
}
