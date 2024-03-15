using AgentAchieve.Core.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AgentAchieve.Core.Domain;

/// <summary>
/// Represents the type of sale (buyer or seller).
/// </summary>
public enum SaleType
{
    [Display(Name = "Buyer Agent Sale")]
    Buyer,
    [Display(Name = "Seller Agent Sale")]
    Seller
}

/// <summary>
/// Represents a sale in the system.
/// </summary>
public class Sale(string ownedById) : OwnerPropertyEntity(ownedById)
{
    /// <summary>
    /// Gets or sets the ID of the property associated with the sale.
    /// </summary>
    public int PropertyId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the client associated with the sale.
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Gets or sets the type of sale (buyer or seller).
    /// </summary>
    public SaleType SaleType { get; set; }

    /// <summary>
    /// Gets or sets the closing date of the sale.
    /// </summary>
    public DateTime ClosingDate { get; set; }

    /// <summary>
    /// Gets or sets the sale price of the property.
    /// </summary>
    [Precision(18, 2)]
    public decimal SalePrice { get; set; }

    /// <summary>
    /// Gets or sets the commission rate for the sale.
    /// </summary>
    [Precision(5, 4)]
    [Range(0.0000, 1.0000)]
    public decimal CommissionRate { get; set; }

    /// <summary>
    /// Gets or sets the property associated with the sale.
    /// </summary>
    public virtual Property? Property { get; set; }

    /// <summary>
    /// Gets or sets the client associated with the sale.
    /// </summary>
    public virtual Client? Client { get; set; }
}
