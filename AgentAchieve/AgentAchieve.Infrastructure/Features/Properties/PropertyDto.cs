using AgentAchieve.Core.Common;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using AgentAchieve.Core.Domain;

namespace AgentAchieve.Infrastructure.Features.Properties;

/// <summary>
/// Represents a data transfer object (DTO) for a property.
/// </summary>
[Description("Properties")]
public class PropertyDto : IEntityPk
{
    /// <summary>
    /// Gets or sets the ID of the property.
    /// </summary>
    [Display(Name = "Id")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the street address of the property.
    /// </summary>
    [Required]
    [Display(Name = "Street")]
    [MaxLength(100)]
    public string? Street { get; set; }

    /// <summary>
    /// Gets or sets the city of the property.
    /// </summary>
    [Required]
    [Display(Name = "City")]
    [MaxLength(50)]
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the state of the property.
    /// </summary>
    [Required]
    [Display(Name = "State")]
    [MaxLength(2)]
    public string? State { get; set; }

    /// <summary>
    /// Gets or sets the zip code of the property.
    /// </summary>
    [Required]
    [Display(Name = "Zip Code")]
    [MaxLength(10)]
    public string? ZipCode { get; set; }

    /// <summary>
    /// Gets or sets the country of the property.
    /// </summary>
    [Display(Name = "Country")]
    [MaxLength(50)]
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the year the property was built.
    /// </summary>
    [Display(Name = "Year Built")]
    public DateTime? YearBuilt { get; set; }

    /// <summary>
    /// Gets or sets the number of bedrooms in the property.
    /// </summary>
    [Display(Name = "Bedrooms")]
    [Range(1, 99, ErrorMessage = "Number of Bedrooms must be between 1 and 99")]
    public int? Bedrooms { get; set; }

    /// <summary>
    /// Gets or sets the number of full bathrooms in the property.
    /// </summary>
    [Display(Name = "Full Bathrooms")]
    [Range(1, 99, ErrorMessage = "Number of Full Bathrooms must be between 1 and 99")]
    public int? FullBathrooms { get; set; }

    /// <summary>
    /// Gets or sets the number of half bathrooms in the property.
    /// </summary>
    [Display(Name = "Half Bathrooms")]
    [Range(1, 99, ErrorMessage = "Number of Half Bathrooms must be between 1 and 99")]
    public int? HalfBathrooms { get; set; }

    /// <summary>
    /// Gets or sets the square footage of the property.
    /// </summary>
    [Display(Name = "Square Footage")]
    [Range(0, int.MaxValue, ErrorMessage = "Square footage must be positive or zero")]
    public int? SquareFootage { get; set; }

    /// <summary>
    /// Gets or sets the number of levels in the property.
    /// </summary>
    [Display(Name = "Number of Levels")]
    [Range(1, 9, ErrorMessage = "Number of levels must be between 1 and 9")]
    public int? NumberOfLevels { get; set; }

    /// <summary>
    /// Gets or sets the lot size of the property in square feet.
    /// </summary>
    [Display(Name = "Lot Size (sqft)")]
    [Range(0, int.MaxValue, ErrorMessage = "Lot size must be positive or zero")]
    public int? LotSize { get; set; }

    /// <summary>
    /// Gets or sets the type of the property.
    /// </summary>
    [Display(Name = "Property Type")]
    public PropertyType? PropertyType { get; set; }

    /// <summary>
    /// Gets or sets the description of the property.
    /// </summary>
    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Display(Name = "Full Address")]
    public string FullAddress => $"{Street} {City}, {State} {ZipCode}";

    /// <summary>
    /// Represents a mapping configuration for the <see cref="Property"/> and <see cref="PropertyDto"/> classes.
    /// </summary>
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Property, PropertyDto>().ReverseMap();
        }
    }
}
