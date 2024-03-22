using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgentAchieve.Infrastructure.Features.SalesGoals;

/// <summary>
/// Represents a service for managing sales goals.
/// </summary>
public class SalesGoalService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SalesGoalService> logger) : ServiceBase<SalesGoal>(unitOfWork, mapper, logger), ISalesGoalService
{

    /// <summary>
    /// Gets all sales goals as DTOs asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of sales goal DTOs.</returns>
    public async Task<IEnumerable<SalesGoalDto>> GetAllDtoAsync()
    {
        return await GetAllDto<SalesGoalDto>().ToListAsync();
    }

    /// <summary>
    /// Gets a sales goal DTO by ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the sales goal.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the sales goal DTO, or null if not found.</returns>
    public async Task<SalesGoalDto?> GetDtoByIdAsync(int id)
    {
        return await GetDtoByIdAsync<SalesGoalDto>(id);
    }

    /// <summary>
    /// Creates a new sales goal asynchronously.
    /// </summary>
    /// <param name="salesGoalDto">The sales goal DTO.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created sales goal DTO.</returns>
    public async Task<SalesGoalDto> CreateSalesGoalAsync(SalesGoalDto salesGoalDto)
    {
        if (salesGoalDto.GoalDate.HasValue)
        {
            // Normalize the GoalDate to the first day of the month at midnight
            salesGoalDto.GoalDate = new DateTime(salesGoalDto.GoalDate.Value.Year, salesGoalDto.GoalDate.Value.Month, 1);
        }

        return await InsertDtoAsync(salesGoalDto);
    }

    /// <summary>
    /// Updates an existing sales goal asynchronously.
    /// </summary>
    /// <param name="salesGoalDto">The sales goal DTO.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated sales goal DTO.</returns>
    public async Task<SalesGoalDto> UpdateSalesGoalAsync(SalesGoalDto salesGoalDto)
    {
        if (salesGoalDto.GoalDate.HasValue)
        {
            // Normalize the GoalDate to the first day of the month at midnight
            salesGoalDto.GoalDate = new DateTime(salesGoalDto.GoalDate.Value.Year, salesGoalDto.GoalDate.Value.Month, 1);
        }

        return await UpdateDtoAsync(salesGoalDto.Id, salesGoalDto);
    }

    /// <summary>
    /// Deletes a sales goal by ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the sales goal to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task DeleteSalesGoalAsync(int id)
    {
        await DeleteAsync(id);
    }

    /// <summary>
    /// Asynchronously determines whether a sales goal with the specified owner ID and goal date already exists.
    /// </summary>
    /// <param name="ownedById">The ID of the owner of the sales goal.</param>
    /// <param name="goalDate">The date of the sales goal.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is true if a sales goal with the specified owner ID and goal date exists; otherwise, false.</returns>
    public async Task<bool> DoesGoalExistAsync(string ownedById, DateTime goalDate)
    {
        // Normalize the GoalDate to the first day of the month at midnight
        goalDate = new DateTime(goalDate.Year, goalDate.Month, 1);

        return await _unitOfWork.GetRepository<SalesGoal>().GetAll()
            .AnyAsync(g => g.OwnedById == ownedById && g.GoalDate.Date == goalDate.Date);
    }
}
