using AgentAchieve.Core.Common;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using AgentAchieve.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace AgentAchieve.Infrastructure.Features.SalesGoals;

/// <summary>
/// Represents a data transfer object (DTO) for a sales goal.
/// </summary>
[Description("Sales Goal")]
public class SalesGoalDto : IEntityPk
{
    /// <summary>
    /// Gets or sets the ID of the sales goal.
    /// </summary>
    [Display(Name = "Id")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the application user id who owns the entity.
    /// </summary>
    [Required]
    [Display(Name = "Agent")]
    public string? OwnedById { get; set; }

    /// <summary>
    /// Gets or sets the date for which this goal applies (month and year).
    /// </summary>
    [Required]
    [Display(Name = "Goal Month/Year")]
    public DateTime? GoalDate { get; set; }

    /// <summary>
    /// Gets or sets the target goal amount for the goal.
    /// </summary>
    [Required]
    [Display(Name = "Goal Amount")]
    [Precision(18, 2)]
    [Range(typeof(decimal), "1", "9999999999999999.99", ErrorMessage = "Goal Amount must be between 1 and 9999999999999999.99")]
    public decimal? GoalAmount { get; set; }

    /// <summary>
    /// Represents a mapping configuration for the <see cref="SalesGoal"/> and <see cref="SalesGoalDto"/> classes.
    /// </summary>
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<SalesGoal, SalesGoalDto>().ReverseMap();
        }
    }
}
