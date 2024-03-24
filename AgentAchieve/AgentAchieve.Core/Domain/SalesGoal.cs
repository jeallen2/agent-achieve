using AgentAchieve.Core.Common;
using Microsoft.EntityFrameworkCore;

namespace AgentAchieve.Core.Domain;

/// <summary>
/// Represents a sales goal with goal date and amount.
/// </summary>
public class SalesGoal(string ownedById) : OwnerPropertyEntity(ownedById)
{
    /// <summary>
    /// Gets or sets the date for which this goal applies (month and year).
    /// </summary>
    public DateTime GoalMonthYear { get; set; }

    /// <summary>
    /// Gets or sets the target sales amount for the goal.
    /// </summary>
    [Precision(18, 2)]
    public decimal SalesGoalAmount { get; set; }
}

