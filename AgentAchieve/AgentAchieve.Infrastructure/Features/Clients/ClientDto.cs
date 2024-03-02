using AgentAchieve.Core.Common;
using AgentAchieve.Core.Domain;
using AutoMapper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgentAchieve.Infrastructure.Features.Clients;
[Description("Clients")]
public class ClientDto : IEntity<int>
{
    //public ClientDto()
    //{
    //}
    //public ClientDto(string firstName, string lastName, string phoneNumber)
    //{
    //    FirstName = firstName;
    //    LastName = lastName;
    //    PhoneNumber = phoneNumber;
    //}

    [Display(Name = "Id")]
    public int Id { get; set; }

    [Required]
    [Display(Name = "First Name")]
    [MaxLength(100)]
    public string? FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    [MaxLength(100)]
    public string? LastName { get; set; }

    [Required]
    [Display(Name = "Phone Number")]
    [MaxLength(15)]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Email")]
    [MaxLength(256)]
    public string? Email { get; set; }

    [Display(Name = "Street")]
    [MaxLength(100)]
    public string? Street { get; set; }

    [Display(Name = "City")]
    [MaxLength(50)]
    public string? City { get; set; }

    [Display(Name = "State")]
    [MaxLength(2)]
    public string? State { get; set; }

    [Display(Name = "Zip Code")]
    [MaxLength(10)]
    public string? ZipCode { get; set; }

    [Display(Name = "Country")]
    [MaxLength(50)]
    public string? Country { get; set; }

    [Display(Name = "Birthdate")]
    public DateTime? Birthdate { get; set; }

    [Display(Name = "Occupation")]
    [MaxLength(50)]
    public string? Occupation { get; set; }

    [Display(Name = "Employer")]
    [MaxLength(100)]
    public string? Employer { get; set; }

    [Display(Name = "Referred By")]
    [MaxLength(100)]
    public string? ReferredBy { get; set; }

    [Display(Name = "Notes")]
    public string? Notes { get; set; }

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
