using AgentAchieve.Core.Common;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using AgentAchieve.Core.Domain;
using Microsoft.EntityFrameworkCore;
using AgentAchieve.Infrastructure.Features.Sales;

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

    [Display(Name = "Agent")]
    public string? OwnerFullName { get; set; }

    /// <summary>
    /// Gets or sets the date for which this goal applies (month and year).
    /// </summary>
    [Required]
    [Display(Name = "Goal Month/Year")]
    [DisplayFormat(DataFormatString = "{0:MMM yyyy}")]
    public DateTime? GoalMonthYear { get; set; }

    /// <summary>
    /// Gets or sets the target goal amount for the goal.
    /// </summary>
    [Required]
    [Display(Name = "Sales Goal")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [Precision(18, 2)]
    [Range(typeof(decimal), "1", "9999999999999999.99", ErrorMessage = "Goal Amount must be between 1 and 9999999999999999.99")]
    public decimal? SalesGoalAmount { get; set; }

    /// <summary>
    /// Gets the total sales achieved for the goal date.
    /// </summary>
    /// <value>
    /// The total sales achieved for the goal date. If the goal date is not set, this property returns 0.
    /// </value>
    [Display(Name = "Total Sales")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [Precision(18, 2)]
    public decimal TotalSales
    {
        get
        {
            if (GoalMonthYear.HasValue)
            {
                return Sales.Where(s => s.ClosingDate.Year == GoalMonthYear.Value.Year && s.ClosingDate.Month == GoalMonthYear.Value.Month)
                            .Sum(s => s.SalePrice ?? 0);
            }
            return 0;
        }
    }


    /// <summary>
    /// Gets the variance between the sales goal and the total sales.
    /// </summary>
    /// <value>
    /// The variance between the sales goal and the total sales. If the sales goal is not set, this property returns 0.
    /// </value>
    [Display(Name = "Over/Under Sales Goal")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [Precision(18, 2)]
    public decimal SalesGoalVariance
    {
        get
        {
            if (SalesGoalAmount.HasValue)
            {
                return TotalSales - SalesGoalAmount.Value;
            }
            return 0;
        }
    }

    /// <summary>
    /// Gets the percentage of the goal achieved.
    /// </summary>
    /// <value>
    /// The percentage of the goal achieved. If the goal amount is not set or is 0, this property returns 0.
    /// </value>
    [Display(Name = "% to Sales Goal")]
    public decimal PercentToSalesGoal
    {
        get
        {
            if (SalesGoalAmount.HasValue && SalesGoalAmount.Value != 0)
            {
                return (TotalSales / SalesGoalAmount.Value) * 100;
            }
            return 0;
        }
    }

    /// <summary>
    /// Gets the commission achieved for the goal date.
    /// </summary>
    /// <value>
    /// The commission achieved for the goal date. If the goal date is not set, this property returns 0.
    /// </value>
    //[Display(Name = "Commission Achieved")]
    //[Precision(18, 2)]
    //public decimal CommissionAchieved
    //{
    //    get
    //    {
    //        if (GoalDate.HasValue)
    //        {
    //            return Sales.Where(s => s.ClosingDate.Year == GoalDate.Value.Year && s.ClosingDate.Month == GoalDate.Value.Month)
    //                        .Sum(s => s.CommissionAmount);
    //        }

    //        return 0;
    //    }
    //}

    /// <summary>
    /// Gets or sets the list of sales associated with the sales goal.
    /// </summary>
    public IEnumerable<SaleDto> Sales { get; set; } = new List<SaleDto>();

    /// <summary>
    /// Represents a mapping configuration for the <see cref="SalesGoal"/> and <see cref="SalesGoalDto"/> classes.
    /// </summary>
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<SalesGoal, SalesGoalDto>()
                .ForMember(dest => dest.Sales, opt => opt.MapFrom(src => src.OwnedBy!.Sales))
                .ForMember(dest => dest.OwnerFullName, opt => opt.MapFrom(src => src.OwnedBy!.FullName))
        .ReverseMap()
                .ForMember(x => x.OwnedBy, opt => opt.Ignore());
        }
    }
}
