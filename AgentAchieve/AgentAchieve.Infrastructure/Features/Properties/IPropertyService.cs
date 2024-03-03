using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Features.Properties;
using AgentAchieve.Infrastructure.Services;

namespace AgentAchieve.Infrastructure.Features.Propertys;

/// <summary>
/// Represents a service for managing propertys.
/// </summary>
public interface IPropertyService : IServiceBase<Property>
{
    /// <summary>
    /// Retrieves all propertys as data transfer objects.
    /// </summary>
    /// <returns>An asynchronous operation that returns a collection of property data transfer objects.</returns>
    Task<IEnumerable<PropertyDto>> GetAllDtoAsync();

    /// <summary>
    /// Retrieves a property data transfer object by its ID.
    /// </summary>
    /// <param name="id">The ID of the property.</param>
    /// <returns>An asynchronous operation that returns the property data transfer object, or null if not found.</returns>
    Task<PropertyDto?> GetDtoByIdAsync(int id);

    /// <summary>
    /// Creates a new property using the provided property data transfer object.
    /// </summary>
    /// <param name="propertyDto">The property data transfer object.</param>
    /// <returns>An asynchronous operation that returns the created property data transfer object.</returns>
    Task<PropertyDto> CreatePropertyAsync(PropertyDto propertyDto);

    /// <summary>
    /// Updates an existing property using the provided property data transfer object.
    /// </summary>
    /// <param name="propertyDto">The property data transfer object.</param>
    /// <returns>An asynchronous operation that returns the updated property data transfer object.</returns>
    Task<PropertyDto> UpdatePropertyAsync(PropertyDto propertyDto);

    /// <summary>
    /// Deletes a property by its ID.
    /// </summary>
    /// <param name="id">The ID of the property to delete.</param>
    /// <returns>An asynchronous operation.</returns>
    Task DeletePropertyAsync(int id);
}
