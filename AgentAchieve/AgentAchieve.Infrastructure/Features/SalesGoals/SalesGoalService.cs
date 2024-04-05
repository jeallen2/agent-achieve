using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        return await GetAllDto<SalesGoalDto>(includes => includes
                                     .Include(sg => sg.OwnedBy)
                                     .ThenInclude(o => o!.Sales)).ToListAsync();
    }

    /// <summary>
    /// Gets a sales goal DTO by ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the sales goal.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the sales goal DTO, or null if not found.</returns>
    public async Task<SalesGoalDto?> GetDtoByIdAsync(int id)
    {
        return await GetAll(includes => includes
                                        .Include(sg => sg.OwnedBy)
                                        .ThenInclude(o => o!.Sales))
            .ProjectTo<SalesGoalDto>(_mapper.ConfigurationProvider)
                                        .FirstOrDefaultAsync(sg => sg.Id == id);
    }

    /// <summary>
    /// Creates a new sales goal asynchronously.
    /// </summary>
    /// <param name="salesGoalDto">The sales goal DTO.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created sales goal DTO.</returns>
    public async Task<SalesGoalDto> CreateSalesGoalAsync(SalesGoalDto salesGoalDto)
    {
        if (salesGoalDto.GoalMonthYear.HasValue)
        {
            // Normalize the GoalMonthYear to the first day of the month at midnight
            salesGoalDto.GoalMonthYear = new DateTime(salesGoalDto.GoalMonthYear.Value.Year, salesGoalDto.GoalMonthYear.Value.Month, 1);
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
        if (salesGoalDto.GoalMonthYear.HasValue)
        {
            // Normalize the GoalMonthYear to the first day of the month at midnight
            salesGoalDto.GoalMonthYear = new DateTime(salesGoalDto.GoalMonthYear.Value.Year, salesGoalDto.GoalMonthYear.Value.Month, 1);
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
    /// <param name="salesGoalDto">The sales goal DTO.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result is true if a sales goal with the specified owner ID and goal date exists; otherwise, false.
    /// If the GoalDate is not set, the method returns true.
    /// The GoalDate is normalized to the first day of the month at midnight.
    /// If the Id property of the salesGoalDto is set (update path), it is excluded from the check.
    /// </returns>
    public async Task<bool> DoesGoalExistAsync(SalesGoalDto salesGoalDto)
    {
        // If the GoalDate is not set, return true
        if (!salesGoalDto.GoalMonthYear.HasValue) return true;

        // Normalize the GoalDate to the first day of the month at midnight
        var goalDate = new DateTime(salesGoalDto.GoalMonthYear.Value.Year, salesGoalDto.GoalMonthYear.Value.Month, 1);

        var query = _unitOfWork.GetRepository<SalesGoal>().GetAll()
            .Where(g => g.OwnedById == salesGoalDto.OwnedById && g.GoalMonthYear.Date == goalDate.Date);

        // If the Id property of the salesGoalDto is set (update path), exclude it from the check
        if (salesGoalDto.Id != 0)
        {
            query = query.Where(g => g.Id != salesGoalDto.Id);
        }

        return await query.AnyAsync();
    }
}
