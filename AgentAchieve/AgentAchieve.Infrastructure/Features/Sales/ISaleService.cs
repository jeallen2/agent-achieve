using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Features.Properties;
using AgentAchieve.Infrastructure.Services;

namespace AgentAchieve.Infrastructure.Features.Sales;

/// <summary>
/// Represents a service for managing sales.
/// </summary>
public interface ISaleService : IServiceBase<Sale>
{
    /// <summary>
    /// Retrieves all sales as data transfer objects.
    /// </summary>
    /// <returns>An asynchronous operation that returns a collection of sale data transfer objects.</returns>
    Task<IEnumerable<SaleDto>> GetAllDtoAsync();

    /// <summary>
    /// Retrieves a sale data transfer object by its ID.
    /// </summary>
    /// <param name="id">The ID of the sale.</param>
    /// <returns>An asynchronous operation that returns the sale data transfer object, or null if not found.</returns>
    Task<SaleDto?> GetDtoByIdAsync(int id);

    /// <summary>
    /// Creates a new sale using the provided sale data transfer object.
    /// </summary>
    /// <param name="saleDto">The sale data transfer object.</param>
    /// <returns>An asynchronous operation that returns the created sale data transfer object.</returns>
    Task<SaleDto> CreateSaleAsync(SaleDto saleDto);

    /// <summary>
    /// Updates an existing sale using the provided sale data transfer object.
    /// </summary>
    /// <param name="saleDto">The sale data transfer object.</param>
    /// <returns>An asynchronous operation that returns the updated sale data transfer object.</returns>
    Task<SaleDto> UpdateSaleAsync(SaleDto saleDto);

    /// <summary>
    /// Deletes a sale by its ID.
    /// </summary>
    /// <param name="id">The ID of the sale to delete.</param>
    /// <returns>An asynchronous operation.</returns>
    Task DeleteSaleAsync(int id);
}
