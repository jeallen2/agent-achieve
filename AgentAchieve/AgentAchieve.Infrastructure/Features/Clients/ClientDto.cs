using AgentAchieve.Core.Common;
using AgentAchieve.Core.Domain;
using AutoMapper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgentAchieve.Infrastructure.Features.Clients;

/// <summary>
/// Represents a data transfer object (DTO) for the Client entity.
/// </summary>
[Description("Clients")]
public class ClientDto : IEntityPk
{
    /// <summary>
    /// Gets or sets the ID of the client.
    /// </summary>
    [Display(Name = "Id")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of the client.
    /// </summary>
    [Required]
    [Display(Name = "First Name")]
    [MaxLength(100)]
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the client.
    /// </summary>
    [Required]
    [Display(Name = "Last Name")]
    [MaxLength(100)]
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the client.
    /// </summary>
    [Required]
    [Display(Name = "Phone Number")]
    [MaxLength(15)]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the email address of the client.
    /// </summary>
    [Display(Name = "Email")]
    [MaxLength(256)]
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the street address of the client.
    /// </summary>
    [Display(Name = "Street")]
    [MaxLength(100)]
    public string? Street { get; set; }

    /// <summary>
    /// Gets or sets the city of the client.
    /// </summary>
    [Display(Name = "City")]
    [MaxLength(50)]
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the state of the client.
    /// </summary>
    [Display(Name = "State")]
    [MaxLength(2)]
    public string? State { get; set; }

    /// <summary>
    /// Gets or sets the zip code of the client.
    /// </summary>
    [Display(Name = "Zip Code")]
    [MaxLength(10)]
    public string? ZipCode { get; set; }

    /// <summary>
    /// Gets or sets the country of the client.
    /// </summary>
    [Display(Name = "Country")]
    [MaxLength(50)]
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the birthdate of the client.
    /// </summary>
    [Display(Name = "Birthdate")]
    public DateTime? Birthdate { get; set; }

    /// <summary>
    /// Gets or sets the occupation of the client.
    /// </summary>
    [Display(Name = "Occupation")]
    [MaxLength(50)]
    public string? Occupation { get; set; }

    /// <summary>
    /// Gets or sets the employer of the client.
    /// </summary>
    [Display(Name = "Employer")]
    [MaxLength(100)]
    public string? Employer { get; set; }

    /// <summary>
    /// Gets or sets the person who referred the client.
    /// </summary>
    [Display(Name = "Referred By")]
    [MaxLength(100)]
    public string? ReferredBy { get; set; }

    /// <summary>
    /// Gets or sets any additional notes about the client.
    /// </summary>
    [Display(Name = "Notes")]
    public string? Notes { get; set; }

    [Display(Name = "Full Name and Phone")]
    public string FullNameAndPhone => $"{LastName}, {FirstName}; Phone: {PhoneNumber}";

    /// <summary>
    /// Represents a mapping configuration for the <see cref="Client"/> and <see cref="ClientDto"/> classes.
    /// </summary>
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
        }
    }
}
