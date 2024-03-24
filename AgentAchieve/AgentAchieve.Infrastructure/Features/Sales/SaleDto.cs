using AgentAchieve.Core.Common;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using AgentAchieve.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace AgentAchieve.Infrastructure.Features.Sales;

/// <summary>
/// Represents a data transfer object (DTO) for a sale.
/// </summary>
[Description("Sales")]
public class SaleDto : IEntityPk
{
    /// <summary>
    /// Gets or sets the ID of the sale.
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
    /// Gets or sets the ID of the property associated with the sale.
    /// </summary>
    [Required]
    [Display(Name = "Property")]
    public int? PropertyId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the client associated with the sale.
    /// </summary>
    [Required]
    [Display(Name = "Client")]
    public int? ClientId { get; set; }

    /// <summary>
    /// Gets or sets the type of sale (buyer or seller).
    /// </summary>
    [Display(Name = "Sale Type")]
    public SaleType SaleType { get; set; }

    /// <summary>
    /// Gets or sets the closing date of the sale.
    /// </summary>
    [Display(Name = "Closing Date")]
    public DateTime ClosingDate { get; set; } = DateTime.Today;

    /// <summary>
    /// Gets or sets the sale price of the property.
    /// </summary>
    [Required]
    [Display(Name = "Sale Price")]
    [Precision(18, 2)]
    [Range(typeof(decimal), "0", "9999999999999999.99", ErrorMessage = "Sale Price must be between 0 and 9999999999999999.99")]
    public decimal? SalePrice { get; set; }


    /// <summary>
    /// Gets or sets the commission rate for the sale.
    /// </summary>
    [Required]
    [Display(Name = "Commission Rate")]
    [Precision(5, 4)]
    [Range(0, 100)]
    public decimal? CommissionRate { get; set; }

    /// <summary>
    /// Gets the commission amount for the sale.
    /// This is calculated as the product of the sale price and the commission rate.
    /// </summary>
    //[Display(Name = "Commission")]
    //public decimal CommissionAmount => SalePrice.GetValueOrDefault() * CommissionRate.GetValueOrDefault();

    /// <summary>
    /// Represents a mapping configuration for the <see cref="Sale"/> and <see cref="SaleDto"/> classes.
    /// </summary>
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Sale, SaleDto>().ReverseMap();
        }
    }
}
