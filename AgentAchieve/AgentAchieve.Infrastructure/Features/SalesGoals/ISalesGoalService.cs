namespace AgentAchieve.Infrastructure.Features.SalesGoals;

/// <summary>
/// Represents a service for managing sales goals.
/// </summary>
public interface ISalesGoalService
{
    /// <summary>
    /// Gets all sales goals as DTOs asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of sales goal DTOs.</returns>
    Task<IEnumerable<SalesGoalDto>> GetAllDtoAsync();

    /// <summary>
    /// Gets a sales goal DTO by ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the sales goal.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the sales goal DTO, or null if not found.</returns>
    Task<SalesGoalDto?> GetDtoByIdAsync(int id);

    /// <summary>
    /// Creates a new sales goal asynchronously.
    /// </summary>
    /// <param name="salesGoalDto">The sales goal DTO.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created sales goal DTO.</returns>
    Task<SalesGoalDto> CreateSalesGoalAsync(SalesGoalDto salesGoalDto);

    /// <summary>
    /// Updates an existing sales goal asynchronously.
    /// </summary>
    /// <param name="salesGoalDto">The sales goal DTO.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated sales goal DTO.</returns>
    Task<SalesGoalDto> UpdateSalesGoalAsync(SalesGoalDto salesGoalDto);

    /// <summary>
    /// Deletes a sales goal by ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the sales goal to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteSalesGoalAsync(int id);

    /// <summary>
    /// Asynchronously determines whether a sales goal with the specified owner ID and goal date already exists.
    /// </summary>
    /// <param name="salesGoalDto">The sales goal DTO.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result is true if a sales goal with the specified owner ID and goal date exists; otherwise, false.
    /// If the GoalDate is not set, the method returns true.
    /// The GoalDate is normalized to the first day of the month at midnight.
    /// If the Id property of the salesGoalDto is set (update path), it is excluded from the check.
    /// </returns>
    Task<bool> DoesGoalExistAsync(SalesGoalDto salesGoalDto);
}
