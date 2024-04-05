using AgentAchieve.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace AgentAchieve.Core.Domain;


/// <summary>
/// Property type.
/// </summary>
public enum PropertyType
{
    [Display(Name = "Single Family Home")]
    SingleFamilyHome,

    [Display(Name = "Condominium")]
    Condo,

    [Display(Name = "Townhouse")]
    Townhouse,

    [Display(Name = "Apartment")]
    Apartment,
    
    [Display(Name = "Land")]
    Land
}

/// <summary>
/// Represents a property with its address and details.
/// </summary>
public class Property(string street, string city, string state, string zipCode) : BaseAuditableEntity
{

    /// <summary>
    /// Gets or sets the street address.
    /// </summary>
    [MaxLength(100)]
    public string Street { get; set; } = street;

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    [MaxLength(50)]
    public string City { get; set; } = city;

    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    [MaxLength(2)]
    public string State { get; set; } = state;

    /// <summary>
    /// Gets or sets the ZIP code.
    /// </summary>
    [MaxLength(10)]
    public string ZipCode { get; set; } = zipCode;

    /// <summary>
    /// Gets or sets the country.
    /// </summary>
    [MaxLength(50)]
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the year the property was built.
    /// </summary>
    public DateTime? YearBuilt { get; set; }

    /// <summary>
    /// Gets or sets the number of bedrooms.
    /// </summary>
    [Range(1, 99, ErrorMessage = "Number of Bedrooms must be between 1 and 99")]
    public int? Bedrooms { get; set; }

    /// <summary>
    /// Gets or sets the number of full bathrooms.
    /// </summary>
    [Range(1, 99, ErrorMessage = "Number of Full Bathrooms must be between 1 and 99")] 
    public int? FullBathrooms { get; set; }

    /// <summary>
    /// Gets or sets the number of half bathrooms.
    /// </summary>
    [Range(1, 99, ErrorMessage = "Number of Half Bathrooms must be between 1 and 99")] 
    public int? HalfBathrooms { get; set; }

    /// <summary>
    /// Gets or sets the square footage of the property.
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Square footage must be positive or zero")]
    public int? SquareFootage { get; set; }

    /// <summary>
    /// Gets or sets the number of levels in the property.
    /// </summary>
    [Range(1, 9, ErrorMessage = "Number of levels must be between 1 and 9")] 
    public int? NumberOfLevels { get; set; }

    /// <summary>
    /// Gets or sets the size of the lot. (sqft)
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Lot size must be positive or zero")]
    public int? LotSize { get; set; }

    /// <summary>
    /// Gets or sets the type of the property.
    /// </summary>
    public PropertyType? PropertyType { get; set; }

    /// <summary>
    /// Gets or sets the description of the property.
    /// </summary>
    public string? Description { get; set; }

    public string FullAddress => $"{Street} {City}, {State} {ZipCode}";
}
